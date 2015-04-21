using Microsoft.Win32;
using Raml.Common.Annotations;
using Raml.Tools;
using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace Raml.Common
{
	/// <summary>
	/// Interaction logic for RamlChooser.xaml
	/// </summary>
	public partial class RamlChooser : INotifyPropertyChanged
	{
		private const string RamlFileExtension = ".raml";
		// action to execute when clicking Ok button (add RAML Reference, Scaffold Web Api, etc.)
		private readonly Action<RamlChooserActionParams> action;
		public string RamlTempFilePath { get; private set; }
		public string RamlOriginalSource { get; set; }

	    public IServiceProvider ServiceProvider { get; set; }

        private bool isContractUseCase;

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
			IsContractUseCase = isContractUseCase;
            btnOk.IsEnabled = false;
			Height = isContractUseCase ? 620 : 525;
			OnPropertyChanged("Height");
		}

		private async void btnChooseFile_Click(object sender, RoutedEventArgs e)
		{
            SelectExistingRamlOption();
			FileDialog fd = new OpenFileDialog();
			fd.DefaultExt = ".raml;*.rml";
			fd.Filter = "RAML files |*.raml;*.rml";

			var opened = fd.ShowDialog();

			if (opened != true)
			{
				return;
			}

			RamlTempFilePath = fd.FileName;
			RamlOriginalSource = fd.FileName;

			var title = Path.GetFileName(fd.FileName);

            var preview = new RamlPreview(ServiceProvider, action, RamlTempFilePath, RamlOriginalSource, title, isContractUseCase);
            preview.FromFile();
            var dialogResult = preview.ShowDialog();
            if(dialogResult == true)
                Close();
		}

        private async void LibraryButton_OnClick(object sender, RoutedEventArgs e)
        {
            SelectExistingRamlOption();
            var rmlLibrary = new RAMLLibraryBrowser();
            var selectedRAMLFile = rmlLibrary.ShowDialog();

            if (selectedRAMLFile.HasValue && selectedRAMLFile.Value)
            {
                var url = rmlLibrary.RAMLFileUrl;

                txtURL.Text = url;

                //TODO: check title !
                var preview = new RamlPreview(ServiceProvider, action, RamlTempFilePath, RamlOriginalSource, "title", isContractUseCase);
                preview.FromURL();
                var dialogResult = preview.ShowDialog();
                if (dialogResult == true)
                    Close();
            }
        }






		private async void GoButton_Click(object sender, RoutedEventArgs e)
		{
            //TODO: check title !
            SelectExistingRamlOption();
            var preview = new RamlPreview(ServiceProvider, action, RamlTempFilePath, txtURL.Text, "title", isContractUseCase);
            preview.FromURL();
            var dialogResult = preview.ShowDialog();
            if(dialogResult == true)
                Close();
		}

		private void NewRaml_Checked(object sender, RoutedEventArgs e)
		{
			var isNewRamlOption = newRamlRadioButton.IsChecked.Value;
			NewOrExistingRamlOptionChanged(isNewRamlOption);
		}

		private void NewOrExistingRamlOptionChanged(bool newRamlIsChecked)
		{
			txtTitle.IsEnabled = newRamlIsChecked;

			txtURL.IsEnabled = !newRamlIsChecked;
			GoButton.IsEnabled = !newRamlIsChecked;
			BrowseButton.IsEnabled = !newRamlIsChecked;
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
			NewRamlFilename = NetNamingMapper.RemoveIndalidChars(txtTitle.Text) + RamlFileExtension;
            NewRamlNamespace = GetNamespace(NewRamlFilename);
			btnOk.IsEnabled = true;
		}

	    public string NewRamlNamespace { get; set; }

	    public string NewRamlFilename { get; set; }

	    private string GetNamespace(string fileName)
        {
            return VisualStudioAutomationHelper.GetDefaultNamespace(ServiceProvider) + "." +
                     NetNamingMapper.GetObjectName(Path.GetFileNameWithoutExtension(fileName));
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

	    private void btnCancel_Click(object sender, RoutedEventArgs e)
	    {
	        Close();
	    }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            var path = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;
            var ramlChooserActionParams = new RamlChooserActionParams(string.Empty, string.Empty, txtTitle.Text, path, NewRamlFilename, NewRamlNamespace, true);
            action(ramlChooserActionParams);
            Close();
        }
	}
}
