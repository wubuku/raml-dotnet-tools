using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using MuleSoft.RAML.Tools.Properties;
using NuGet.VisualStudio;
using Raml.Common;

namespace MuleSoft.RAML.Tools
{
	public class RamlReferenceService
	{
		private readonly IServiceProvider serviceProvider;
		private readonly string nugetPackagesSource = Settings.Default.NugetPackagesSource;
		private readonly string newtonsoftJsonPackageId = Settings.Default.NewtonsoftJsonPackageId;
		private readonly string newtonsoftJsonPackageVersion = Settings.Default.NewtonsoftJsonPackageVersion;
		private readonly string webApiClientPackageId = Settings.Default.WebApiClientPackageId;
		private readonly string webApiClientPackageVersion = Settings.Default.WebApiClientPackageVersion;
		private readonly string ramlClientCommonPackageId = Settings.Default.RAMLClientCommonPackageId;
		private readonly string ramlClientCommonPackageVersion = Settings.Default.RAMLClientCommonPackageVersion;
		public readonly static string ApiReferencesFolderName = Settings.Default.ApiReferencesFolderName;

		private readonly TemplatesManager templatesManager = new TemplatesManager();

		public RamlReferenceService(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
		}

		public void AddRamlReference(RamlChooserActionParams parameters)
		{
			try
			{
				ActivityLog.LogInformation(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, "Add RAML Reference process started");
				var dte = serviceProvider.GetService(typeof (SDTE)) as DTE;
				var proj = VisualStudioAutomationHelper.GetActiveProject(dte);

				InstallNugetDependencies(proj);
				ActivityLog.LogInformation(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, "Nuget Dependencies installed");

				AddFilesToProject(parameters.RamlFilePath, proj, parameters.TargetNamespace, parameters.RamlSource, parameters.TargetFileName);
				ActivityLog.LogInformation(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource, "Files added to project");
			}
			catch (Exception ex)
			{
				ActivityLog.LogError(VisualStudioAutomationHelper.RamlVsToolsActivityLogSource,
					VisualStudioAutomationHelper.GetExceptionInfo(ex));
				MessageBox.Show("Error when trying to add the RAML reference. " + ex.Message);
				throw;
			}
		}

		private void InstallNugetDependencies(Project proj)
		{
			var componentModel = (IComponentModel)serviceProvider.GetService(typeof(SComponentModel));
			var installerServices = componentModel.GetService<IVsPackageInstallerServices>();
			var installer = componentModel.GetService<IVsPackageInstaller>();

			if (!installerServices.IsPackageInstalled(proj, newtonsoftJsonPackageId))
			{
				installer.InstallPackage(nugetPackagesSource, proj, newtonsoftJsonPackageId, newtonsoftJsonPackageVersion, false);
			}

			// Web Api Client (to get HttpClient)
			if (!installerServices.IsPackageInstalled(proj, webApiClientPackageId))
			{
				installer.InstallPackage(nugetPackagesSource, proj, webApiClientPackageId, webApiClientPackageVersion, false);
			}

			// RAML.Tools.Core
			if (!installerServices.IsPackageInstalled(proj, ramlClientCommonPackageId))
			{
				installer.InstallPackage(nugetPackagesSource, proj, ramlClientCommonPackageId, ramlClientCommonPackageVersion, false);
			}
		}

		private void AddFilesToProject(string ramlSourceFile, Project proj, string targetNamespace, string ramlOriginalSource, string targetFileName)
		{
			if(!File.Exists(ramlSourceFile))
				throw new FileNotFoundException("RAML file not found " + ramlSourceFile);

			if(Path.GetInvalidFileNameChars().Any(targetFileName.Contains))
				throw new ArgumentException("Specified filename has invalid chars: " + targetFileName);

			var destFolderName = Path.GetFileNameWithoutExtension(targetFileName);
			var apiRefsFolderPath = Path.GetDirectoryName(proj.FullName) + Path.DirectorySeparatorChar + ApiReferencesFolderName + Path.DirectorySeparatorChar;
			var destFolderPath = apiRefsFolderPath + destFolderName + Path.DirectorySeparatorChar;
			var apiRefsFolderItem = VisualStudioAutomationHelper.AddFolderIfNotExists(proj, ApiReferencesFolderName);

			var destFolderItem = VisualStudioAutomationHelper.AddFolderIfNotExists(apiRefsFolderItem, destFolderName, destFolderPath);

			InstallerServices.AddRefFile(ramlSourceFile, targetNamespace, ramlOriginalSource, destFolderPath, targetFileName);

			var ramlProjItem = InstallerServices.AddOrUpdateRamlFile(ramlSourceFile, destFolderPath, destFolderItem, targetFileName);

			templatesManager.CopyClientTemplateToProjectFolder(apiRefsFolderPath);

			ramlProjItem.Properties.Item("CustomTool").Value = string.Empty; // to cause a refresh when file already exists
			ramlProjItem.Properties.Item("CustomTool").Value = "RamlClientTool";
		}

	}
}