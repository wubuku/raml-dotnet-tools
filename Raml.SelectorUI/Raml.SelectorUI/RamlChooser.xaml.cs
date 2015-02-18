using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.VisualStudio.Shell;
using Microsoft.Win32;
using Raml.Common.Annotations;
using Raml.Parser;
using Raml.Parser.Expressions;
using Raml.Tools;

namespace Raml.Common
{
	/// <summary>
	/// Interaction logic for RamlChooser.xaml
	/// </summary>
	public partial class RamlChooser : INotifyPropertyChanged
	{
		private const string RamlFileExtension = ".raml";
		// action to execute when clicking Ok button (add RAML Reference, Scaffold Web Api, etc)
		private readonly Action<RamlChooserActionParams> action;
		private bool isContractUseCase;
		private readonly RamlIncludesManager includesManager = new RamlIncludesManager();
		public string RamlTempFilePath { get; private set; }
		public string RamlOriginalSource { get; set; }

		public string TargetNamespace { get { return txtNamespace.Text; } }
		public IServiceProvider ServiceProvider { get; set; }

		private bool IsContractUseCase
		{
			get { return isContractUseCase; }
			set
			{
				if (value.Equals(isContractUseCase)) return;
				isContractUseCase = value;
				OnPropertyChanged("ContractUseCaseVisibility");
			}
		}

		public Visibility ContractUseCaseVisibility
		{
			get
			{
				return IsContractUseCase ? Visibility.Visible : Visibility.Collapsed;
			}
		}

		public RamlChooser(IServiceProvider serviceProvider, Action<RamlChooserActionParams> action, string title, bool isContractUseCase)
		{
			this.action = action;
			ServiceProvider = serviceProvider;
			InitializeComponent();
			Title = title;
			btnOk.IsEnabled = false;
			IsContractUseCase = isContractUseCase;

			Height = isContractUseCase ? 420 : 325;
			OnPropertyChanged("Height");
		}

		private async void btnChooseFile_Click(object sender, RoutedEventArgs e)
		{
			FileDialog fd = new OpenFileDialog();
			fd.DefaultExt = ".raml;*.rml";
			fd.Filter = "RAML files |*.raml;*.rml";

			var opened = fd.ShowDialog();
			progressBar.Visibility = Visibility.Visible;

			if (opened != true)
			{
				progressBar.Visibility = Visibility.Hidden;
				return;
			}
			DoEvents();

			RamlTempFilePath = fd.FileName;
			RamlOriginalSource = fd.FileName;

			addressText.Text = "File: " + Path.GetFileName(fd.FileName);
			txtFileName.Text = Path.GetFileName(fd.FileName);

			try
			{
				var result = includesManager.Manage(RamlTempFilePath, Path.GetTempPath());
				var parser = new RamlParser();
				var document = await parser.LoadRamlAsync(result.ModifiedContents);

				SetPreview(document);
			}
			catch (Exception ex)
			{
				ShowErrorStopProgressAndDisableOk("Error while parsing raml file. " + ex.Message);
				ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, VisualStudioAutomationHelper.GetExceptionInfo(ex));
			}
		}

		private void SetPreview(RamlDocument document)
		{
			Dispatcher.Invoke(() =>
			{
				try
				{
					SelectExistingRamlOption();
					ResourcesLabel.Text = GetResourcesPreview(document);
					StopProgress();
					btnOk.IsEnabled = true;
					SetNamespace(RamlTempFilePath);

					if (NetNamingMapper.HasIndalidChars(txtFileName.Text))
					{
						ShowErrorStopProgressAndDisableOk("The specied file name has invalid chars");
						txtFileName.Focus();
					}
				}
				catch (Exception ex)
				{
					ShowErrorStopProgressAndDisableOk("Error while parsing raml file. " + ex.Message);
					ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, VisualStudioAutomationHelper.GetExceptionInfo(ex));
				}
			});
		}

		private void btnOk_Click(object sender, RoutedEventArgs e)
		{
			StartProgress();
			DoEvents();

			if (!txtFileName.Text.ToLowerInvariant().EndsWith(RamlFileExtension))
			{
				ShowErrorStopProgressAndDisableOk("Error: the file must have the .raml extension.");
				DialogResult = false;
				return;				
			}

			if (!IsContractUseCase && !File.Exists(RamlTempFilePath))
			{
				ShowErrorStopProgressAndDisableOk("Error: the specified file does not exist.");
				DialogResult = false;
				return;
			}

			var path = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;

			try
			{
				ResourcesLabel.Text = "Processing. Please wait..." + Environment.NewLine + Environment.NewLine;
				
				// Execute action (add RAML Reference, Scaffold Web Api, etc)
				var parameters = new RamlChooserActionParams(RamlOriginalSource, RamlTempFilePath, txtTitle.Text, path,
					txtFileName.Text, txtNamespace.Text, chkDoNotScaffold.IsChecked);
				action(parameters);

				ResourcesLabel.Text += "Succeeded";
				StopProgress();
				btnOk.IsEnabled = true;
				DialogResult = true;
				Close();
			}
			catch (Exception ex)
			{
				ShowErrorStopProgressAndDisableOk("Error: " + ex.Message);

				ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, VisualStudioAutomationHelper.GetExceptionInfo(ex));
			}
		}

		private void ShowErrorStopProgressAndDisableOk(string errorMessage)
		{
			ShowErrorAndStopProgress(errorMessage);
			btnOk.IsEnabled = false;
		}

		private void ShowErrorAndStopProgress(string errorMessage)
		{
			ResourcesLabel.Text = errorMessage;
			StopProgress();
		}

		private void StopProgress()
		{
			progressBar.Visibility = Visibility.Hidden;
			Mouse.OverrideCursor = null;
		}

		private static string GetResourcesPreview(RamlDocument ramlDoc)
		{
			return GetChildResources(ramlDoc.Resources, 0);
		}

		private static string GetChildResources(IEnumerable<Resource> resources, int level)
		{
			var output = string.Empty;
			foreach (var childResource in resources)
			{
				output += new string(' ', level * 2) + childResource.RelativeUri;
				if (childResource.Resources.Any())
				{
					output += Environment.NewLine;
					output += GetChildResources(childResource.Resources, level + 1);
				}
				else
				{
					output += Environment.NewLine;
				}
			}
			return output;
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}

		private async void GoButton_Click(object sender, RoutedEventArgs e)
		{
			StartProgress();
			DoEvents();

			try
			{
				var url = addressText.Text;
				var result = includesManager.Manage(url, Path.GetTempPath());

				var raml = result.ModifiedContents;
				var parser = new RamlParser();

				var ramlDocument = await parser.LoadRamlAsync(raml);

				var filename = Path.GetFileName(url);

				if (string.IsNullOrEmpty(filename))
					filename = "reference.raml";

				if (!filename.ToLowerInvariant().EndsWith(RamlFileExtension))
					filename += RamlFileExtension;

				txtFileName.Text = NetNamingMapper.RemoveIndalidChars(Path.GetFileNameWithoutExtension(filename)) + RamlFileExtension;

				var path = Path.Combine(Path.GetTempPath(), filename);
				File.WriteAllText(path, raml);
				RamlTempFilePath = path;
				RamlOriginalSource = url;

				SetPreview(ramlDocument);

				btnOk.IsEnabled = true;
				StopProgress();
			}
			catch (UriFormatException uex)
			{
				ShowErrorAndStopProgress(uex.Message);
			}
			catch (HttpRequestException rex)
			{
				ShowErrorAndStopProgress(GetFriendlyMessage(rex));
				ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, VisualStudioAutomationHelper.GetExceptionInfo(rex));
			}
			catch (Exception ex)
			{
				ShowErrorAndStopProgress(ex.Message);
				ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, VisualStudioAutomationHelper.GetExceptionInfo(ex));
			}
		}

		private void StartProgress()
		{
			progressBar.Visibility = Visibility.Visible;
			btnOk.IsEnabled = false;
			Mouse.OverrideCursor = System.Windows.Input.Cursors.Wait;
		}

		private static string GetFriendlyMessage(HttpRequestException rex)
		{
			if (rex.Message.Contains("404"))
				return "Could not find specified URL. Server responded with Not Found (404) status code";

			return rex.Message;
		}

		#region refresh UI
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public void DoEvents()
		{
			Dispatcher.Invoke(new Action(() => { }), DispatcherPriority.ContextIdle, null);

			//DispatcherFrame frame = new DispatcherFrame();
			//Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
			//	new DispatcherOperationCallback(ExitFrame), frame);
			//Dispatcher.PushFrame(frame);
		}

		public object ExitFrame(object f)
		{
			((DispatcherFrame)f).Continue = false;

			return null;
		}
		#endregion

		private void NewRaml_Checked(object sender, RoutedEventArgs e)
		{
			var isNewRamlOption = newRamlRadioButton.IsChecked.Value;
			NewOrExistingRamlOptionChanged(isNewRamlOption);
		}

		private void NewOrExistingRamlOptionChanged(bool newRamlIsChecked)
		{
			txtTitle.IsEnabled = newRamlIsChecked;

			addressText.IsEnabled = !newRamlIsChecked;
			GoButton.IsEnabled = !newRamlIsChecked;
			BrowseButton.IsEnabled = !newRamlIsChecked;

			chkDoNotScaffold.IsChecked = newRamlIsChecked;
			chkDoNotScaffold.IsEnabled = !newRamlIsChecked;
		}

		private void BrowseExisting_Checked(object sender, RoutedEventArgs e)
		{
			var isNewRamlOption = !existingRamlRadioButton.IsChecked.Value;
			NewOrExistingRamlOptionChanged(isNewRamlOption);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		private void Title_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			btnOk.IsEnabled = false;
			if (string.IsNullOrWhiteSpace(txtTitle.Text)) 
				return;

			SelectNewRamlOption();
			txtFileName.Text = NetNamingMapper.RemoveIndalidChars(txtTitle.Text) + RamlFileExtension;
			SetNamespace(txtFileName.Text);
			btnOk.IsEnabled = true;
		}

		private void SetNamespace(string fileName)
		{
			var ns = VisualStudioAutomationHelper.GetDefaultNamespace(ServiceProvider) + "." +
			         NetNamingMapper.GetObjectName(Path.GetFileNameWithoutExtension(fileName));
			txtNamespace.Text = ns;
		}

		private void SelectExistingRamlOption()
		{
			existingRamlRadioButton.IsChecked = true;
			newRamlRadioButton.IsChecked = false;
		}

		private void SelectNewRamlOption()
		{
			newRamlRadioButton.IsChecked = true;
			existingRamlRadioButton.IsChecked = false;
		}

	}
}
