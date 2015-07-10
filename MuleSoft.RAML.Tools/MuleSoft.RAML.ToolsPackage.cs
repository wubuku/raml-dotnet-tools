using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using MuleSoft.RAML.Tools.Properties;
using MuleSoft.RAML.Tools.RamlPropertiesExtender;
using NuGet.VisualStudio;
using Raml.Common;
using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;

namespace MuleSoft.RAML.Tools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidMuleSoft_RAML_ToolsPackagePkgString)]
	[ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")]
    public sealed class MuleSoft_RAML_ToolsPackage : Package, IVsThreadedWaitDialogCallback
    {
        private CommandID addReferenceInApiFolderCmdId;
	    private CommandID updateReferenceCmdId;
	    private CommandID updateRamlContractCommandId;
        private CommandID enableRamlMetadataOutputCommandId;
        private CommandID disableRamlMetadataOutputCommandId;
        private CommandID editRamlPropertiesCmdId;
        //private CommandID extractRAMLCommandId;
        private static Events events;
        private static DocumentEvents documentEvents;
        private IVsThreadedWaitDialog3 attachingDialog;

        public MuleSoft_RAML_ToolsPackage()
	    {
		    var message = string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this);
		    Debug.WriteLine(message);

#if DEBUG
        ServicePointManager.ServerCertificateValidationCallback = (obj, certificate, chain, errors) => true;
#endif
      }

        protected override void Initialize()
        {
	        var message = string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this);
	        Debug.WriteLine (message);
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
	        if (null == mcs) return;

	        // Add RAML Reference command in References
	        var addRamlRefCommandId = new CommandID(GuidList.guidMuleSoft_RAML_ReferencesNode, (int)PkgCmdIDList.cmdRAMLGenerator);
	        var addRamlRefCommand = new MenuCommand(AddRamlReferenceCallback, addRamlRefCommandId );
	        mcs.AddCommand( addRamlRefCommand );

            // Add RAML Reference command in Api References Folder
            addReferenceInApiFolderCmdId = new CommandID(GuidList.guidMuleSoft_RAML_FolderNode, (int)PkgCmdIDList.cmdRAMLGenerator2);
            var addReferenceCommand = new OleMenuCommand(AddRamlReferenceCallback, addReferenceInApiFolderCmdId);
            addReferenceCommand.BeforeQueryStatus += AddRamlRefCommand_BeforeQueryStatus;
            mcs.AddCommand(addReferenceCommand);

			// Update RAML Reference command
	        updateReferenceCmdId = new CommandID(GuidList.guidMuleSoft_RAML_FileNode, (int)PkgCmdIDList.cmdUpdateRAMLReference);
	        var updateReferenceCommand = new OleMenuCommand(UpdateRamlRefCallback, updateReferenceCmdId);
	        updateReferenceCommand.BeforeQueryStatus += UpdateRamlRefCommand_BeforeQueryStatus;
			mcs.AddCommand(updateReferenceCommand);

			// Add RAML Contract command
			var addRamlContractCmdId = new CommandID(GuidList.guidMuleSoft_RAML_ProjectNode, (int)PkgCmdIDList.cmdAddContract);
			var addRamlContractCommand = new OleMenuCommand(AddRamlContractCallback, addRamlContractCmdId);
			addRamlContractCommand.BeforeQueryStatus += AddRamlContractCommandOnBeforeQueryStatus;
			mcs.AddCommand(addRamlContractCommand);

			// Update RAML from source (Contract/Server) command
			updateRamlContractCommandId = new CommandID(GuidList.guidMuleSoft_RAML_CmdUpdateRAMLContract, (int)PkgCmdIDList.cmdUpdateRAMLContract);
			var updateRamlCommand = new OleMenuCommand(UpdateRamlContractCallback, updateRamlContractCommandId);
			updateRamlCommand.BeforeQueryStatus += UpdateRAMLCommandOnBeforeQueryStatus;
			mcs.AddCommand(updateRamlCommand);

            // Enable RAML metadata output (RAML WebApiExplorer) command
            enableRamlMetadataOutputCommandId = new CommandID(GuidList.guidMuleSoft_RAML_EnableMetadataOutput, (int)PkgCmdIDList.cmdEnableMetadataOutput);
            var enableRamlMetadataOutput = new OleMenuCommand(EnableRamlMetadataOutputCallback, enableRamlMetadataOutputCommandId);
            enableRamlMetadataOutput.BeforeQueryStatus += AddReverseEngineeringCommandOnBeforeQueryStatus;
            mcs.AddCommand(enableRamlMetadataOutput);

            // Disable RAML metadata output (RAML WebApiExplorer) command
            disableRamlMetadataOutputCommandId = new CommandID(GuidList.guidMuleSoft_RAML_DisableMetadataOutput, (int)PkgCmdIDList.cmdDisableMetadataOutput);
            var disableRamlMetadataOutput = new OleMenuCommand(DisableRamlMetadataOutputCallback, disableRamlMetadataOutputCommandId);
            disableRamlMetadataOutput.BeforeQueryStatus += RemoveReverseEngineeringCommandOnBeforeQueryStatus;
            mcs.AddCommand(disableRamlMetadataOutput);

            //// Extract RAML (RAML WebApiExplorer) command
            //extractRAMLCommandId = new CommandID(GuidList.guidMuleSoft_RAML_ExtractRAML, (int)PkgCmdIDList.cmdExtractRAML);
            //var extractRAMLCommand = new OleMenuCommand(ExtractRAMLCallback, extractRAMLCommandId);
            //extractRAMLCommand.BeforeQueryStatus += ExtractRAMLCommandOnBeforeQueryStatus;
            //mcs.AddCommand(extractRAMLCommand);

            // trigger scaffold when RAML document gets saved
            var dte = ServiceProvider.GlobalProvider.GetService(typeof(SDTE)) as DTE;
            events = dte.Events;
            documentEvents = events.DocumentEvents;
            documentEvents.DocumentSaved += RamlScaffoldService.TriggerScaffoldOnRamlChanged;

            // show RAML metadata (namespace, source) in Properties Windows and allow to edit
            dte.ObjectExtenders.RegisterExtenderProvider(
                VSLangProj.PrjBrowseObjectCATID.prjCATIDCSharpFileBrowseObject, "RamlPropertiesExtender",
                new RamlPropertiesExtenderProvider());
        }

        private void DisableRamlMetadataOutputCallback(object sender, EventArgs e)
        {
            ChangeCommandStatus(enableRamlMetadataOutputCommandId, false);

            StartProgressBar("Disable RAML metada output", "Uninstalling...", "Processing...");

            var service = new ReverseEngineeringService(ServiceProvider.GlobalProvider);
            service.RemoveReverseEngineering();

            StopProgressBar();

            ChangeCommandStatus(enableRamlMetadataOutputCommandId, true);
        }

        //private void ExtractRAMLCallback(object sender, EventArgs e)
        //{
        //    ChangeCommandStatus(extractRAMLCommandId, false);

        //    var service = new ReverseEngineeringService(ServiceProvider.GlobalProvider);
        //    service.ExtractRAML();

        //    ChangeCommandStatus(extractRAMLCommandId, true);
        //}

        private void EnableRamlMetadataOutputCallback(object sender, EventArgs e)
        {
            ChangeCommandStatus(enableRamlMetadataOutputCommandId, false);

            StartProgressBar("Enable RAML metada output", "Installing...", "Processing...");

            var service = new ReverseEngineeringService(ServiceProvider.GlobalProvider);
            service.AddReverseEngineering();

            StopProgressBar();

            System.Diagnostics.Process.Start("https://github.com/mulesoft-labs/raml-dotnet-tools#metadata");
            ChangeCommandStatus(enableRamlMetadataOutputCommandId, true);
        }

        private void UpdateRamlContractCallback(object sender, EventArgs e)
	    {
			ChangeCommandStatus(updateRamlContractCommandId, false);

			// Get the file path
			uint itemid;
			IVsHierarchy hierarchy;
			if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
			string ramlFilePath;
			((IVsProject)hierarchy).GetMkDocument(itemid, out ramlFilePath);

			var ramlScaffoldUpdater = new RamlScaffoldService(new T4Service(ServiceProvider.GlobalProvider), ServiceProvider.GlobalProvider);
			ramlScaffoldUpdater.UpdateRaml(ramlFilePath);

			ChangeCommandStatus(updateRamlContractCommandId, true);
	    }

	    private void AddRamlContractCallback(object sender, EventArgs e)
	    {
		    var ramlScaffoldUpdater = new RamlScaffoldService(new T4Service(ServiceProvider.GlobalProvider), ServiceProvider.GlobalProvider);
		    var frm = new RamlChooser(ServiceProvider.GlobalProvider, ramlScaffoldUpdater.AddContract, "Add RAML Contract", true, Settings.Default.RAMLExchangeUrl);
		    frm.ShowDialog();
	    }

		private void AddRamlReferenceCallback(object sender, EventArgs e)
		{
			var generationServices = new RamlReferenceService(ServiceProvider.GlobalProvider);
			var ramlChooser = new RamlChooser(this, generationServices.AddRamlReference, "Add RAML Reference", false, Settings.Default.RAMLExchangeUrl);
			ramlChooser.ShowDialog();
		}

		private void UpdateRamlRefCallback(object sender, EventArgs e)
		{
			ChangeCommandStatus(updateReferenceCmdId, false);

            // Get the file path
            uint itemid;
            IVsHierarchy hierarchy;
            if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
            string ramlFilePath;
            ((IVsProject)hierarchy).GetMkDocument(itemid, out ramlFilePath);

		    var templatesManager = new TemplatesManager();
		    var ramlFolder = Path.GetDirectoryName(ramlFilePath).TrimEnd(Path.DirectorySeparatorChar);
		    var generatedFolderPath = ramlFolder.Substring(0, ramlFolder.LastIndexOf(Path.DirectorySeparatorChar) + 1);
		    if(!templatesManager.ConfirmWhenIncompatibleClientTemplate(generatedFolderPath))
                return;

            //if (Unauthorized(ramlFilePath))
            //{
            //    var generationServices = new RamlReferenceService(ServiceProvider.GlobalProvider);
            //    var ramlChooser = new RamlChooser(this, generationServices.AddRamlReference, "Update RAML Reference", false,
            //        Settings.Default.RAMLExchangeUrl);
            //    ramlChooser.ShowDialog();
            //}
            //else
            //{
		        var dte = (DTE2) GetService(typeof (SDTE));
		        dte.ExecuteCommand("Project.RunCustomTool");
            //}

		    ChangeCommandStatus(updateReferenceCmdId, true);
		}


        //private bool Unauthorized(string ramlFilePath)
        //{
        //    var refFilePath = InstallerServices.GetRefFilePath(ramlFilePath);
        //    var ramlSource = RamlReferenceReader.GetRamlSource(refFilePath);
        //    if (!ramlSource.StartsWith("http"))
        //        return false;

        //    var client = new HttpClient();
        //    var task = client.SendAsync(new HttpRequestMessage(HttpMethod.Get, ramlSource));
        //    task.WaitWithPumping();
        //    var result = task.ConfigureAwait(false).GetAwaiter().GetResult();
        //    return result.StatusCode == HttpStatusCode.Unauthorized;
        //}

        private void AddRamlRefCommand_BeforeQueryStatus(object sender, EventArgs e)
        {
            ShowOrHideCommandAddRefApiFolder(sender);
        }

		private void UpdateRAMLCommandOnBeforeQueryStatus(object sender, EventArgs e)
		{
			ShowOrHideCommandContract(sender);
		}

        //private void ExtractRAMLCommandOnBeforeQueryStatus(object sender, EventArgs e)
        //{
        //    var menuCommand = sender as OleMenuCommand;
        //    if (menuCommand == null) return;

        //    ShowAndEnableCommand(menuCommand, false);

        //    if (!IsWebApiCoreInstalled())
        //        return;

        //    ShowAndEnableCommand(menuCommand, true);
        //}

        private void AddReverseEngineeringCommandOnBeforeQueryStatus(object sender, EventArgs e)
        {
            var menuCommand = sender as OleMenuCommand;
            if (menuCommand == null) return;

            ShowAndEnableCommand(menuCommand, false);

            if (!IsWebApiCoreInstalled())
                return;

            if (IsWebApiExplorerInstalled())
                return;

            ShowAndEnableCommand(menuCommand, true);
        }

        private void RemoveReverseEngineeringCommandOnBeforeQueryStatus(object sender, EventArgs e)
        {
            var menuCommand = sender as OleMenuCommand;
            if (menuCommand == null) return;

            ShowAndEnableCommand(menuCommand, false);

            if (!IsWebApiExplorerInstalled())
                return;

            ShowAndEnableCommand(menuCommand, true);
        }


        private void AddRamlContractCommandOnBeforeQueryStatus(object sender, EventArgs eventArgs)
		{
			var menuCommand = sender as OleMenuCommand;
			if (menuCommand == null) return;

			ShowAndEnableCommand(menuCommand, false);

			if (!IsWebApiCoreInstalled())
				return;

			ShowAndEnableCommand(menuCommand, true);
		}

        private bool IsWebApiExplorerInstalled()
        {
            var dte = ServiceProvider.GlobalProvider.GetService(typeof(SDTE)) as DTE;
            var proj = VisualStudioAutomationHelper.GetActiveProject(dte);
            var componentModel = (IComponentModel)ServiceProvider.GlobalProvider.GetService(typeof(SComponentModel));
            var installerServices = componentModel.GetService<IVsPackageInstallerServices>();
            var isWebApiCoreInstalled = installerServices.IsPackageInstalled(proj, "RAML.WebApiExplorer");
            return isWebApiCoreInstalled;
        }

	    private static bool IsWebApiCoreInstalled()
	    {
		    var dte = ServiceProvider.GlobalProvider.GetService(typeof (SDTE)) as DTE;
		    var proj = VisualStudioAutomationHelper.GetActiveProject(dte);
		    var componentModel = (IComponentModel) ServiceProvider.GlobalProvider.GetService(typeof (SComponentModel));
		    var installerServices = componentModel.GetService<IVsPackageInstallerServices>();
		    var isWebApiCoreInstalled = installerServices.IsPackageInstalled(proj, "Microsoft.AspNet.WebApi.Core");
		    return isWebApiCoreInstalled;
	    }

	    private void UpdateRamlRefCommand_BeforeQueryStatus(object sender, EventArgs e)
		{
			ShowOrHideCommand(sender, RamlReferenceService.ApiReferencesFolderName);
		}

		private void ChangeCommandStatus(CommandID commandId, bool enable)
		{
			var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
			if (mcs == null) return;

			var menuCmd = mcs.FindCommand(commandId);
			if (menuCmd != null) menuCmd.Enabled = enable;
		}

        private static void ShowOrHideCommandAddRefApiFolder(object sender)
        {
            // get the menu that fired the event
            var menuCommand = sender as OleMenuCommand;
            if (menuCommand == null) return;

            ShowAndEnableCommand(menuCommand, false);

            IVsHierarchy hierarchy;
            uint itemid;

            if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
            // Get the file path
            string itemFullPath;
            ((IVsProject)hierarchy).GetMkDocument(itemid, out itemFullPath);

            var folder = Path.GetDirectoryName(itemFullPath);
            if (!folder.EndsWith(Settings.Default.ApiReferencesFolderName))
                return;

            ShowAndEnableCommand(menuCommand, true);
        }


		private static void ShowOrHideCommand(object sender, string containingFolderName)
		{
			// get the menu that fired the event
			var menuCommand = sender as OleMenuCommand;
			if (menuCommand == null) return;

			ShowAndEnableCommand(menuCommand, false);

			IVsHierarchy hierarchy;
			uint itemid;

			if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
			// Get the file path
			string itemFullPath;
			((IVsProject)hierarchy).GetMkDocument(itemid, out itemFullPath);
			var transformFileInfo = new FileInfo(itemFullPath);

			var endsWithExtension = transformFileInfo.Name.EndsWith(".raml");
			
			// if not leave the menu hidden
			if (!endsWithExtension) return;

			if(!itemFullPath.Contains(containingFolderName))
				return;
			
			var folder = Path.GetDirectoryName(itemFullPath);
			if (folder.EndsWith(InstallerServices.IncludesFolderName))
				return;

			ShowAndEnableCommand(menuCommand, true);
		}

	    private static void ShowOrHideCommandContract(object sender)
	    {
			// get the menu that fired the event
		    var menuCommand = sender as OleMenuCommand;
		    if (menuCommand == null) return;

		    ShowAndEnableCommand(menuCommand, false);

		    IVsHierarchy hierarchy;
		    uint itemid;

		    if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
		    // Get the file path
		    string itemFullPath;
		    ((IVsProject) hierarchy).GetMkDocument(itemid, out itemFullPath);
		    var transformFileInfo = new FileInfo(itemFullPath);

		    var endsWithExtension = transformFileInfo.Name.EndsWith(".raml");

		    // if not leave the menu hidden
		    if (!endsWithExtension) return;

			if (itemFullPath.Contains(RamlReferenceService.ApiReferencesFolderName))
				return;

			var folder = Path.GetDirectoryName(itemFullPath);
			if (folder.EndsWith(InstallerServices.IncludesFolderName))
				return;

			if (!IsWebApiCoreInstalled())
				return;

			ShowAndEnableCommand(menuCommand, true);
	    }

        //private static void ShowOrHideCommandReverseEngineering(object sender)
        //{
        //    // get the menu that fired the event
        //    var menuCommand = sender as OleMenuCommand;
        //    if (menuCommand == null) return;

        //    ShowAndEnableCommand(menuCommand, false);

        //    IVsHierarchy hierarchy;
        //    uint itemid;

        //    if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
        //    // Get the file path
        //    string itemFullPath;
        //    ((IVsProject)hierarchy).GetMkDocument(itemid, out itemFullPath);
        //    var transformFileInfo = new FileInfo(itemFullPath);

        //    var endsWithExtension = transformFileInfo.Name.EndsWith(".raml");

        //    // if not leave the menu hidden
        //    if (!endsWithExtension) return;

        //    if (itemFullPath.Contains(RamlReferenceService.ApiReferencesFolderName))
        //        return;

        //    var folder = Path.GetDirectoryName(itemFullPath);
        //    if (folder.EndsWith(InstallerServices.IncludesFolderName))
        //        return;

        //    if (!IsWebApiCoreInstalled())
        //        return;

        //    ShowAndEnableCommand(menuCommand, true);
        //}

	    private static void ShowAndEnableCommand(OleMenuCommand menuCommand, bool visible)
	    {
		    menuCommand.Visible = visible;
		    menuCommand.Enabled = visible;
	    }

	    public static bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
		{
			hierarchy = null;
			itemid = VSConstants.VSITEMID_NIL;

		    var monitorSelection = GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
			var solution = GetGlobalService(typeof(SVsSolution)) as IVsSolution;
			if (monitorSelection == null || solution == null)
			{
				return false;
			}

		    var hierarchyPtr = IntPtr.Zero;
			var selectionContainerPtr = IntPtr.Zero;

			try
			{
				IVsMultiItemSelect multiItemSelect;
				var hr = monitorSelection.GetCurrentSelection(out hierarchyPtr, out itemid, out multiItemSelect, out selectionContainerPtr);

				if (ErrorHandler.Failed(hr) || hierarchyPtr == IntPtr.Zero || itemid == VSConstants.VSITEMID_NIL)
				{
					// there is no selection
					return false;
				}

				// multiple items are selected
				if (multiItemSelect != null) return false;

				// there is a hierarchy root node selected, thus it is not a single item inside a project

				if (itemid == VSConstants.VSITEMID_ROOT) return false;

				hierarchy = Marshal.GetObjectForIUnknown(hierarchyPtr) as IVsHierarchy;
				if (hierarchy == null) return false;

				Guid guidProjectId;

				if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out guidProjectId)))
				{
					return false; // hierarchy is not a project inside the Solution if it does not have a ProjectID Guid
				}

				// if we got this far then there is a single project item selected
				return true;
			}
			finally
			{
				if (selectionContainerPtr != IntPtr.Zero)
				{
					Marshal.Release(selectionContainerPtr);
				}

				if (hierarchyPtr != IntPtr.Zero)
				{
					Marshal.Release(hierarchyPtr);
				}
			}
		}

        public void OnCanceled()
        {
            StopProgressBar();
        }

        private void StopProgressBar()
        {
            if (attachingDialog != null)
            {
                var canceled = 0;
                attachingDialog.EndWaitDialog(out canceled);
                attachingDialog = null;
            }
        }

        private void StartProgressBar(string title, string message, string progressMessage)
        {
            var dialogFactory = GetService(typeof(SVsThreadedWaitDialogFactory)) as IVsThreadedWaitDialogFactory;
            IVsThreadedWaitDialog2 dialog = null;
            if (dialogFactory != null)
            {
                dialogFactory.CreateInstance(out dialog);
            }

            attachingDialog = (IVsThreadedWaitDialog3)dialog;

            attachingDialog.StartWaitDialogWithCallback(title,
                message, string.Empty, null,
                progressMessage, true, 0,
                true, 0, 0, this);
        }

    }
}
