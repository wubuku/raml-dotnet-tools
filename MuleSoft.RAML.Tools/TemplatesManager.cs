using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using MuleSoft.RAML.Tools.Properties;

namespace MuleSoft.RAML.Tools
{
	public class TemplatesManager
	{
		private const string DefaultTemplateVersion = "0.1";
		private const int HashLineIndex = 1;
		private const int VersionLineIndex = 2;
		private const string HashPrefix = "// hash:";
		private const string VersionPrefix = "// version:";
		private readonly string ServerTemplatesVersion = Settings.Default.ServerTemplatesVersion;
		private readonly string ClientTemplatesVersion = Settings.Default.ServerTemplatesVersion;

		public void CopyServerTemplateToProjectFolder(string generatedFolderPath, string templateName)
		{
			var extensionPath = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;
			var sourceTemplateFolder = Path.Combine(extensionPath,
				"Templates" + Path.DirectorySeparatorChar + "RAMLWebApi2Scaffolder" + Path.DirectorySeparatorChar);

			CopyTemplateToProjectFolder(generatedFolderPath, templateName, sourceTemplateFolder, ServerTemplatesVersion);
		}

		public void CopyClientTemplateToProjectFolder(string generatedFolderPath)
		{
			var templateName = Settings.Default.ClientT4TemplateName;
			var extensionPath = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;
			CopyTemplateToProjectFolder(generatedFolderPath, templateName, extensionPath, ClientTemplatesVersion);
		}

		private void CopyTemplateToProjectFolder(string generatedFolderPath, string templateName, string templatesSourceFolder, string version)
		{
			var destTemplateFilePath = Path.Combine(generatedFolderPath, templateName);
			var sourceTemplateFilePath = Path.Combine(templatesSourceFolder, templateName);

			if (File.Exists(destTemplateFilePath))
			{
				ManageExistingTemplate(templateName, destTemplateFilePath, sourceTemplateFilePath, version);
			}
			else
			{
				CopyTemplateAndAddMetadata(sourceTemplateFilePath, destTemplateFilePath, version);
			}
		}

		private void ManageExistingTemplate(string templateName, string destTemplateFilePath, string sourceTemplateFilePath, string version)
		{
			if (HasMetadata(destTemplateFilePath))
			{
				ManageExistingTemplateWithMetadata(templateName, destTemplateFilePath, sourceTemplateFilePath, version);
			}
			else
			{
				CopyTemplateAndAddMetadata(sourceTemplateFilePath, destTemplateFilePath, version);
			}
		}

		private void ManageExistingTemplateWithMetadata(string templateName, string destTemplateFilePath,
			string sourceTemplateFilePath, string version)
		{
			if (HasTemplateChanged(destTemplateFilePath))
			{
				ManageTemplateThatWasCustomized(templateName, destTemplateFilePath, sourceTemplateFilePath, version);
			}
			else
			{
				CopyTemplateAndAddMetadata(sourceTemplateFilePath, destTemplateFilePath, version);
			}
		}

		private void ManageTemplateThatWasCustomized(string templateName, string destTemplateFilePath,
			string sourceTemplateFilePath, string version)
		{
			if (IsTheSameVersion(destTemplateFilePath, version))
				return;

			if (!IsVersionCompatible(destTemplateFilePath, version))
			{
				MessageBox.Show(
					string.Format(
						"The current tool is not compatible with this version of the template {0}."
						+ " We need to overwrite it with the new version. You can do a backup before clicking OK.", templateName),
					"Warning",
					MessageBoxButton.OK, MessageBoxImage.Warning);

				CopyTemplateAndAddMetadata(sourceTemplateFilePath, destTemplateFilePath, version);
			}
			else
			{
				var dialogResult =
					MessageBox.Show(
						string.Format("Template {0} has changed. Do you want to override it and loose your changes?", templateName),
						"Confirmation",
						MessageBoxButton.YesNo, MessageBoxImage.Question);

				if (dialogResult == MessageBoxResult.Yes)
					CopyTemplateAndAddMetadata(sourceTemplateFilePath, destTemplateFilePath, version);
			}
		}

		private bool IsVersionCompatible(string templateFilePath, string newTemplatesVersion)
		{
			var installedVersion = GetVersion(templateFilePath);
			var numbers = installedVersion.Split('.');
			var mayorInstalled = Convert.ToInt16(numbers[0]);
			var newMayor = Convert.ToInt16(newTemplatesVersion.Split('.')[0]);
			return mayorInstalled == newMayor;
		}

		private string GetVersion(string templateFilePath)
		{
			var lines = File.ReadAllLines(templateFilePath).ToList();
			if (!lines[VersionLineIndex].Contains(VersionPrefix))
				return DefaultTemplateVersion;

			return lines[VersionLineIndex].Replace(VersionPrefix + " ", string.Empty).Trim();
		}

		private void CopyTemplateAndAddMetadata(string sourceTemplateFilePath, string destTemplateFilePath, string version)
		{
			File.Copy(sourceTemplateFilePath, destTemplateFilePath, true);
			AddTemplateMetadata(version, destTemplateFilePath);
		}

		private bool IsTheSameVersion(string templateFilePath, string version)
		{
			var currentVersion = GetVersion(templateFilePath);
			return currentVersion == version;
		}

		private bool HasTemplateChanged(string templateFilePath)
		{
			var lines = File.ReadAllLines(templateFilePath).ToList();
			if (!lines[HashLineIndex].Contains(HashPrefix))
				return false;

			var hash = lines[HashLineIndex].Replace(HashPrefix + " ", string.Empty);
			RemoveMetadataLines(lines);
			var contentWithoutMetadata = string.Join(Environment.NewLine, lines);
			bool hasChanged;

			using (var md5 = MD5.Create())
			{
				hasChanged = !VerifyMd5Hash(md5, contentWithoutMetadata, hash);
			}
			return hasChanged;
		}

		private static bool VerifyMd5Hash(HashAlgorithm md5, string input, string hash)
		{
			var hashOfInput = GetMd5Hash(md5, input);
			var comparer = StringComparer.OrdinalIgnoreCase;
			return 0 == comparer.Compare(hashOfInput, hash);
		}

		private static void RemoveMetadataLines(List<string> lines)
		{
			lines.RemoveAt(0);
			lines.RemoveAt(0);
			lines.RemoveAt(0);
			lines.RemoveAt(0);
		}

		private bool HasMetadata(string templateFilePath)
		{
			var lines = File.ReadAllLines(templateFilePath).ToList();
			return lines[HashLineIndex].Contains(HashPrefix);
		}

		private static string ComputeHash(string templateFilePath)
		{
			var lines = File.ReadAllLines(templateFilePath).ToList();
			if (lines[HashLineIndex].Contains(HashPrefix))
			{
				RemoveMetadataLines(lines);
			}
			var contentWithoutMetadata = string.Join(Environment.NewLine, lines);

			string hash;
			using (var md5 = MD5.Create())
			{
				hash = GetMd5Hash(md5, contentWithoutMetadata);
			}

			return hash;
		}

		private void AddTemplateMetadata(string version, string templateFilePath)
		{
			var lines = File.ReadAllLines(templateFilePath).ToList();
			if (lines[HashLineIndex].Contains(HashPrefix))
			{
				RemoveMetadataLines(lines);
			}
			lines.Insert(0, "#>");
			lines.Insert(0, VersionPrefix + " " + version);
			lines.Insert(0, HashPrefix + " " + ComputeHash(templateFilePath));
			lines.Insert(0, "<#");
			
			File.WriteAllLines(templateFilePath, lines);
		}

		public void EnsureClientVersionCompatibility(string templateFolder)
		{
			var templateFilePath = Path.Combine(templateFolder, Settings.Default.ClientT4TemplateName);
			if (!IsVersionCompatible(templateFilePath, ClientTemplatesVersion))
				CopyClientTemplateToProjectFolder(templateFolder);
		}

		public void EnsureServerVersionCompatibility(string templateFolder, string templateName)
		{
			var templateFilePath = Path.Combine(templateFolder, templateName);
			if (!IsVersionCompatible(templateFilePath, ServerTemplatesVersion))
				CopyServerTemplateToProjectFolder(templateFolder, templateName);
		}


		private static string GetMd5Hash(HashAlgorithm md5Hash, string input)
		{
			var data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
			var sBuilder = new StringBuilder();
			foreach (var b in data)
			{
				sBuilder.Append(b.ToString("x2"));
			}
			return sBuilder.ToString();
		}

	}
}