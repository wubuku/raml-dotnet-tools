using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using NuGet.VisualStudio;
using Raml.Common;

namespace MuleSoft.RAML.Tools
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidMuleSoft_RAML_ToolsPackagePkgString)]
	[ProvideAutoLoad("f1536ef8-92ec-443c-9ed7-fdadf150da82")]
    public sealed class MuleSoft_RAML_ToolsPackage : Package
    {
	    private CommandID updateReferenceCmdId;
	    private CommandID implementContractCommandId;
	    private CommandID updateRAMLContractCommandId;

	    public MuleSoft_RAML_ToolsPackage()
	    {
		    var message = string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this);
		    Debug.WriteLine(message);
	    }

        protected override void Initialize()
        {
	        var message = string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this);
	        Debug.WriteLine (message);
            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
	        if (null == mcs) return;

	        // Add RAML Reference command
	        var addRamlRefCommandId = new CommandID(GuidList.guidMuleSoft_RAML_ReferencesNode, (int)PkgCmdIDList.cmdRAMLGenerator);
	        var addRamlRefCommand = new MenuCommand(AddRamlReferenceCallback, addRamlRefCommandId );
	        mcs.AddCommand( addRamlRefCommand );

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

			// Implement RAML Contract command
	        implementContractCommandId = new CommandID(GuidList.guidMuleSoft_RAML_CmdImplementContract, (int) PkgCmdIDList.cmdImplementContract);
	        var implementContractCommand = new OleMenuCommand(ImplementContractCallback, implementContractCommandId);
			implementContractCommand.BeforeQueryStatus += ImplementContractCommandOnBeforeQueryStatus;
			mcs.AddCommand(implementContractCommand);

			// Update RAML from source (Contract/Server) command
			updateRAMLContractCommandId = new CommandID(GuidList.guidMuleSoft_RAML_CmdUpdateRAMLContract, (int)PkgCmdIDList.cmdUpdateRAMLContract);
			var updateRAMLCommand = new OleMenuCommand(UpdateRAMLContractCallback, updateRAMLContractCommandId);
			updateRAMLCommand.BeforeQueryStatus += UpdateRAMLCommandOnBeforeQueryStatus;
			mcs.AddCommand(updateRAMLCommand);

        }

	    private void UpdateRAMLContractCallback(object sender, EventArgs e)
	    {
			ChangeCommandStatus(updateRAMLContractCommandId, false);

			// Get the file path
			uint itemid;
			IVsHierarchy hierarchy;
			if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
			string ramlFilePath;
			((IVsProject)hierarchy).GetMkDocument(itemid, out ramlFilePath);

			var ramlScaffoldUpdater = new RamlScaffoldService(new T4Service(ServiceProvider.GlobalProvider), ServiceProvider.GlobalProvider);
			ramlScaffoldUpdater.UpdateRaml(ramlFilePath);

			ChangeCommandStatus(updateRAMLContractCommandId, true);
	    }

	    private void AddRamlContractCallback(object sender, EventArgs e)
	    {
		    var ramlScaffoldUpdater = new RamlScaffoldService(new T4Service(ServiceProvider.GlobalProvider), ServiceProvider.GlobalProvider);
		    var frm = new RamlChooser(ServiceProvider.GlobalProvider, ramlScaffoldUpdater.AddContract, "Add RAML Contract", true);
		    frm.ShowDialog();
	    }



	    private void ImplementContractCallback(object sender, EventArgs e)
		{
			ChangeCommandStatus(implementContractCommandId, false);

			// Get the file path
			uint itemid;
			IVsHierarchy hierarchy;
			if (!IsSingleProjectItemSelection(out hierarchy, out itemid)) return;
			string ramlFilePath;
			((IVsProject)hierarchy).GetMkDocument(itemid, out ramlFilePath);

		    var refFilePath = Path.Combine(Path.GetDirectoryName(ramlFilePath), Path.GetFileNameWithoutExtension(ramlFilePath) + ".ref");
			var targetNamespace = RamlReferenceReader.GetRamlNamespace(refFilePath);

		    try
		    {
			    var scaffolder = new RamlScaffoldService(new T4Service(ServiceProvider.GlobalProvider),
				    ServiceProvider.GlobalProvider);
			    scaffolder.Scaffold(ramlFilePath, targetNamespace, Path.GetFileName(ramlFilePath));
		    }
		    catch (Exception ex)
		    {
			    MessageBox.Show(ex.Message, "Error");
		    }

			ChangeCommandStatus(implementContractCommandId, true);
		}

		private void AddRamlReferenceCallback(object sender, EventArgs e)
		{
			var generationServices = new RamlReferenceService(ServiceProvider.GlobalProvider);
			var ramlChooser = new RamlChooser(this, generationServices.AddRamlReference, "Add RAML Reference", false);
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

			var dte = (DTE2)GetService(typeof(SDTE));
			dte.ExecuteCommand("Project.RunCustomTool");

			ChangeCommandStatus(updateReferenceCmdId, true);
		}

		private void UpdateRAMLCommandOnBeforeQueryStatus(object sender, EventArgs e)
		{
			ShowOrHideCommandContract(sender);
		}

	    private void ImplementContractCommandOnBeforeQueryStatus(object sender, EventArgs eventArgs)
	    {
			ShowOrHideCommandContract(sender);
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

	    private static void ShowAndEnableCommand(OleMenuCommand menuCommand, bool visible)
	    {
		    menuCommand.Visible = visible;
		    menuCommand.Enabled = visible;
	    }

	    public static bool IsSingleProjectItemSelection(out IVsHierarchy hierarchy, out uint itemid)
		{
			hierarchy = null;
			itemid = VSConstants.VSITEMID_NIL;

		    var monitorSelection = Package.GetGlobalService(typeof(SVsShellMonitorSelection)) as IVsMonitorSelection;
			var solution = Package.GetGlobalService(typeof(SVsSolution)) as IVsSolution;
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

				Guid guidProjectID;

				if (ErrorHandler.Failed(solution.GetGuidOfProject(hierarchy, out guidProjectID)))
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

    }
}
