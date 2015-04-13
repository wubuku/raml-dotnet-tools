using Microsoft.VisualStudio.Shell;
using System;
using System.IO;
using System.Security.Permissions;
using System.Windows;
using System.Windows.Threading;

namespace Raml.Common
{
    /// <summary>
    /// Interaction logic for RamlPreview.xaml
    /// </summary>
    public partial class RamlPreview
    {
        private const string RamlFileExtension = ".raml";
        // action to execute when clicking Ok button (add RAML Reference, Scaffold Web Api, etc.)
        private readonly Action<RamlChooserActionParams> action;

        public string RamlTempFilePath { get; private set; }
        public string RamlOriginalSource { get; private set; }
        public string RamlTitle { get; private set; }

        private bool IsContractUseCase { get; set; }

        public RamlPreview(Action<RamlChooserActionParams> action, string ramlTempFilePath, string ramlOriginalSource, string ramlTitle, bool isContractUseCase)
        {
            RamlTempFilePath = ramlTempFilePath;
            RamlOriginalSource = ramlOriginalSource;
            RamlTitle = ramlTitle;
            IsContractUseCase = isContractUseCase;
            this.action = action;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            //StartProgress();
            DoEvents();

            if (!txtFileName.Text.ToLowerInvariant().EndsWith(RamlFileExtension))
            {
                ShowErrorAndDisableOk("Error: the file must have the .raml extension.");
                DialogResult = false;
                return;
            }

            if (!IsContractUseCase && !File.Exists(RamlTempFilePath))
            {
                ShowErrorAndDisableOk("Error: the specified file does not exist.");
                DialogResult = false;
                return;
            }

            var path = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;

            try
            {
                ResourcesLabel.Text = "Processing. Please wait..." + Environment.NewLine + Environment.NewLine;

                // Execute action (add RAML Reference, Scaffold Web Api, etc)
                var parameters = new RamlChooserActionParams(RamlOriginalSource, RamlTempFilePath, RamlTitle, path,
                    txtFileName.Text, txtNamespace.Text, false);
                action(parameters);

                ResourcesLabel.Text += "Succeeded";
                //StopProgress();
                btnOk.IsEnabled = true;
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                ShowErrorAndDisableOk("Error: " + ex.Message);

                ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, VisualStudioAutomationHelper.GetExceptionInfo(ex));
            }
        }



        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ShowErrorAndDisableOk(string errorMessage)
        {
            ShowError(errorMessage);
            btnOk.IsEnabled = false;
        }

        private void ShowError(string errorMessage)
        {
            ResourcesLabel.Text = errorMessage;
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

    }
}
