using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Raml.Tools;

namespace Raml.Common
{
	public class RamlIncludesManager
	{
		private readonly char[] IncludeDirectiveTrimChars = { ' ', '"', '}', ']', ',' };
		private const string IncludeDirective = "!include";
        private IDictionary<string, Task> downloadFileTasks = new Dictionary<string, Task>();

	    private HttpClient client;
	    private HttpClient Client
	    {
	        get
	        {
                if(client == null)
                    client = new HttpClient();
	            return client;
	        }
	    }


	    public RamlIncludesManagerResult Manage(string ramlSource, string destinationFolder, bool confirmOverrite = false)
		{
			string path;

			string[] lines;
			if (ramlSource.StartsWith("http"))
			{
				var destinationFilePath = GetDestinationFilePath(Path.GetTempPath(), ramlSource);

			    var task = DownloadFileAsync(ramlSource, destinationFilePath);
                task.WaitWithPumping();
			    var uri = task.ConfigureAwait(false).GetAwaiter().GetResult();

				path = uri.AbsolutePath;
				if (ramlSource.Contains("/"))
					path = ramlSource.Substring(0, ramlSource.LastIndexOf("/", StringComparison.InvariantCulture) + 1);

				lines = File.ReadAllLines(destinationFilePath);
			}
			else
			{
				path = Path.GetDirectoryName(ramlSource);
				lines = File.ReadAllLines(ramlSource);
			}

			var includedFiles = new Collection<string>();

			// if there are any includes and the folder does not exists, create it
			if (lines.Any(l => l.Contains(IncludeDirective)) && !Directory.Exists(destinationFolder))
				Directory.CreateDirectory(destinationFolder);

			Manage(lines, destinationFolder, includedFiles, path, confirmOverrite);

		    //Task.WaitAll(downloadFileTasks.ToArray());

			return new RamlIncludesManagerResult(string.Join(Environment.NewLine, lines), includedFiles);
		}

		private void Manage(IList<string> lines, string destinationFolder, ICollection<string> includedFiles, string path, bool confirmOvewrite)
		{
		    var scopeIncludedFiles = new Collection<string>();
			for (var i = 0; i < lines.Count; i++)
			{
				var line = lines[i];
				if (!line.Contains(IncludeDirective))
					continue;

				var indexOfInclude = line.IndexOf(IncludeDirective, System.StringComparison.Ordinal);
				var includeSource = line.Substring(indexOfInclude + IncludeDirective.Length).Trim(IncludeDirectiveTrimChars);

				var destinationFilePath = GetDestinationFilePath(destinationFolder, includeSource);

			    if (!includedFiles.Contains(destinationFilePath))
			    {

			        if (IsWebSource(path, includeSource))
			        {
			            var fullPathIncludeSource = GetFullWebSource(path, includeSource);
			            DownloadFile(fullPathIncludeSource, destinationFilePath);
			        }
			        else
			        {
			            var fullPathIncludeSource = includeSource;
			            // if relative does not exist, try with full path
			            if (!File.Exists(includeSource))
			                fullPathIncludeSource = Path.Combine(path, includeSource);


			            // copy file to dest folder
			            if (File.Exists(destinationFilePath) && confirmOvewrite)
			            {
			                var dialogResult = InstallerServices.ShowConfirmationDialog(Path.GetFileName(destinationFilePath));
			                if (dialogResult == MessageBoxResult.Yes)
			                {
			                    if (File.Exists(destinationFilePath))
			                        new FileInfo(destinationFilePath).IsReadOnly = false;

			                    File.Copy(fullPathIncludeSource, destinationFilePath, true);
			                }
			            }
			            else
			            {
			                if (File.Exists(destinationFilePath))
			                    new FileInfo(destinationFilePath).IsReadOnly = false;

			                File.Copy(fullPathIncludeSource, destinationFilePath, true);
			            }
			        }

                    includedFiles.Add(destinationFilePath);
                    scopeIncludedFiles.Add(destinationFilePath);
			    }

			    // replace old include for new include
				lines[i] = lines[i].Replace(includeSource, GetPathWithoutDriveLetter(destinationFilePath));
			}

		    foreach (var includedFile in scopeIncludedFiles)
		    {
                // parse include for other includes
		        Task.WaitAll(downloadFileTasks[includedFile]);
                var nestedFileLines = File.ReadAllLines(includedFile);
                Manage(nestedFileLines, destinationFolder, includedFiles, path, confirmOvewrite);
		    }
		}

		private static string GetPathWithoutDriveLetter(string destinationFilePath)
		{
			return destinationFilePath.Substring(1,1) == ":" ? destinationFilePath.Substring(2) : destinationFilePath;
		}

		private string GetDestinationFilePath(string destinationFolder, string includeSource)
		{
			var filename = GetFileName(includeSource);
			var destinationFilePath = Path.Combine(destinationFolder, filename);
			var doubleDirSeparator = Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture) +
			                         Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture);
			destinationFilePath = destinationFilePath.Replace(doubleDirSeparator, Path.DirectorySeparatorChar.ToString(CultureInfo.InvariantCulture));
			return destinationFilePath;
		}

		private Uri DownloadFile(string ramlSourceUrl, string destinationFilePath, bool confirmOvewrite = false)
		{
			Uri uri;
			if (!Uri.TryCreate(ramlSourceUrl, UriKind.Absolute, out uri))
				throw new UriFormatException("Invalid URL: " + ramlSourceUrl);

            downloadFileTasks.Add(destinationFilePath, GetContentsAsync(uri).ContinueWith(task => WriteFile(destinationFilePath, confirmOvewrite, task.Result)));

		    //WriteFile(destinationFilePath, confirmOvewrite, contents);

			return uri;
		}

        private async Task<Uri> DownloadFileAsync(string ramlSourceUrl, string destinationFilePath, bool confirmOvewrite = false)
        {
            Uri uri;
            if (!Uri.TryCreate(ramlSourceUrl, UriKind.Absolute, out uri))
                throw new UriFormatException("Invalid URL: " + ramlSourceUrl);

            await GetContentsAsync(uri).ContinueWith(task => WriteFile(destinationFilePath, confirmOvewrite, task.Result));

            //WriteFile(destinationFilePath, confirmOvewrite, contents);

            return uri;
        }


	    private static void WriteFile(string destinationFilePath, bool confirmOvewrite, string contents)
	    {
	        if (File.Exists(destinationFilePath) && confirmOvewrite)
	        {
	            var dialogResult = InstallerServices.ShowConfirmationDialog(Path.GetFileName(destinationFilePath));
	            if (dialogResult == MessageBoxResult.Yes)
	            {
	                if (File.Exists(destinationFilePath))
	                    new FileInfo(destinationFilePath).IsReadOnly = false;
	                File.WriteAllText(destinationFilePath, contents);
	            }
	        }
	        else
	        {
	            if (File.Exists(destinationFilePath))
	                new FileInfo(destinationFilePath).IsReadOnly = false;
	            File.WriteAllText(destinationFilePath, contents);
	        }
	    }

	    private static string GetFileName(string ramlSource)
		{
			var filename = Path.GetFileName(ramlSource);
			if (string.IsNullOrWhiteSpace(filename))
				filename = NetNamingMapper.GetObjectName(ramlSource) + ".raml"; //TODO: check
			return filename;
		}

		private static string GetFullWebSource(string path, string includeSource)
		{
			if (!includeSource.StartsWith("http"))
				includeSource = path.EndsWith("/") || includeSource.StartsWith("/") ? path + includeSource : path + "/" + includeSource;

			return includeSource;
		}

		private static bool IsWebSource(string path, string includeSource)
		{
			return includeSource.StartsWith("http") || (!string.IsNullOrWhiteSpace(path) && path.StartsWith("http"));
		}

        public async Task<string> GetContentsAsync(Uri uri)
        {
            var downloadTask = await Client.GetStringAsync(uri).ConfigureAwait(false);
            return downloadTask;
        }
	}
}