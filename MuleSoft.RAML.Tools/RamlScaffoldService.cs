using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using EnvDTE;
using Microsoft.VisualStudio.Shell.Interop;
using MuleSoft.RAML.Tools.Properties;
using Raml.Common;
using Raml.Tools.WebApiGenerator;

namespace MuleSoft.RAML.Tools
{
	public class RamlScaffoldService
	{
		private const string RamlSpecVersion = "0.8";
		private const string ControllerDeclarationTemplateName = "ApiBaseControllerTemplate";
		private const string ControllerInterfaceTemplateName = "ApiControllerInterfaceTemplate";
		private const string ControllerImplementationTemplateName = "ApiControllerTemplate";
		private const string ObjectTemplateName = "ObjectTemplate";

		private readonly string folderName = Settings.Default.ContractsFolderName;
		private readonly IT4Service t4Service;
		private readonly IServiceProvider serviceProvider;

		public RamlScaffoldService(IT4Service t4Service, IServiceProvider serviceProvider)
		{
			this.t4Service = t4Service;
			this.serviceProvider = serviceProvider;
		}

		public void AddContract(RamlChooserActionParams parameters)
		{
			var dte = serviceProvider.GetService(typeof(SDTE)) as DTE;
			var proj = VisualStudioAutomationHelper.GetActiveProject(dte);
			var folderItem = VisualStudioAutomationHelper.AddFolderIfNotExists(proj, folderName);
			var generatedFolderPath = Path.GetDirectoryName(proj.FullName) + Path.DirectorySeparatorChar + folderName + Path.DirectorySeparatorChar;

			if (string.IsNullOrWhiteSpace(parameters.RamlSource) && !string.IsNullOrWhiteSpace(parameters.RamlTitle))
				AddEmptyContract(parameters.TargetFileName, parameters.RamlTitle, folderItem, generatedFolderPath, parameters.TargetNamespace, parameters.TargetFileName);
			else
				AddContractFromFile(parameters.RamlFilePath, parameters.TargetNamespace, parameters.RamlSource,
					parameters.DoNotScaffold, folderItem, generatedFolderPath, parameters.TargetFileName);
		}

		private void AddContractFromFile(string ramlFilePath, string targetNamespace, string ramlSource, bool? doNotScaffold, ProjectItem folderItem, string folderPath, string targetFilename)
		{
			InstallerServices.AddRefFile(ramlFilePath, targetNamespace, ramlSource, folderPath, targetFilename);

			var includesFolderPath = folderPath + Path.DirectorySeparatorChar + InstallerServices.IncludesFolderName;

			var includesManager = new RamlIncludesManager();
			var result = includesManager.Manage(ramlSource, includesFolderPath, confirmOverrite: true);

			var includesFolderItem = folderItem.ProjectItems.Cast<ProjectItem>().FirstOrDefault(i => i.Name == InstallerServices.IncludesFolderName);
			if (includesFolderItem == null)
				includesFolderItem = folderItem.ProjectItems.AddFolder(InstallerServices.IncludesFolderName);

			foreach (var file in result.IncludedFiles)
			{
				includesFolderItem.ProjectItems.AddFromFile(file);
			}

			//var existingIncludeItems = includesFolderItem.ProjectItems.Cast<ProjectItem>();
			//var oldIncludedFiles = existingIncludeItems.Where(item => !result.IncludedFiles.Contains(item.FileNames[0]));
			//InstallerServices.RemoveSubItemsAndAssociatedFiles(oldIncludedFiles);

			var ramlProjItem = AddOrUpdateRamlFile(result.ModifiedContents, folderItem, folderPath, Path.GetFileName(ramlFilePath));
			InstallerServices.RemoveSubItemsAndAssociatedFiles(ramlProjItem);

			if (doNotScaffold == null || !doNotScaffold.Value)
				Scaffold(ramlSource, targetNamespace, Path.GetFileName(ramlFilePath));
		}

		private static ProjectItem AddOrUpdateRamlFile(string modifiedContents, ProjectItem folderItem, string folderPath, string ramlFileName)
		{
			ProjectItem ramlProjItem;
			var ramlDestFile = Path.Combine(folderPath, ramlFileName);

			if (File.Exists(ramlDestFile))
			{
				var dialogResult = InstallerServices.ShowConfirmationDialog(ramlFileName);

				if (dialogResult == MessageBoxResult.Yes)
				{
					File.WriteAllText(ramlDestFile, modifiedContents);
					ramlProjItem = folderItem.ProjectItems.AddFromFile(ramlDestFile);
				}
				else
				{
					ramlProjItem = folderItem.ProjectItems.Cast<ProjectItem>().FirstOrDefault(i => i.Name == ramlFileName);
					if (ramlProjItem == null)
						ramlProjItem = folderItem.ProjectItems.AddFromFile(ramlDestFile);
				}
			}
			else
			{
				File.WriteAllText(ramlDestFile, modifiedContents);
				ramlProjItem = folderItem.ProjectItems.AddFromFile(ramlDestFile);
			}
			return ramlProjItem;
		}

		private void AddEmptyContract(string filename, string title, ProjectItem folderItem, string folderPath, string targetNamespace, string targetFilename)
		{
			
			var newContractFile = Path.Combine(folderPath, filename);
			var contents = CreateNewRamlContents(title);

			InstallerServices.AddRefFile(newContractFile, targetNamespace, newContractFile, folderPath, targetFilename);

			if (File.Exists(newContractFile))
			{
				var dialogResult = InstallerServices.ShowConfirmationDialog(filename);
				if (dialogResult == MessageBoxResult.Yes)
				{
					File.WriteAllText(newContractFile, contents);
					folderItem.ProjectItems.AddFromFile(newContractFile);
				}
				else
				{
					var item = folderItem.ProjectItems.Cast<ProjectItem>().FirstOrDefault(i => i.Name == newContractFile);
					if (item == null)
						folderItem.ProjectItems.AddFromFile(newContractFile);
				}
			}
			else
			{
				File.WriteAllText(newContractFile, contents);
				folderItem.ProjectItems.AddFromFile(newContractFile);
			}
		}

		private static string CreateNewRamlContents(string title)
		{
			var contents = "#%RAML " + RamlSpecVersion + Environment.NewLine +
			               "title: " + title + Environment.NewLine;
			return contents;
		}

		public void Scaffold(string ramlSource, string targetNamespace, string ramlFileName)
		{
			var data = RamlScaffolderHelper.GetRamlData(ramlSource, targetNamespace);
			if(data == null || data.Model == null)
				return;

			var model = data.Model;

			var dte = serviceProvider.GetService(typeof(SDTE)) as DTE;
			var proj = VisualStudioAutomationHelper.GetActiveProject(dte);

			var folderItem = VisualStudioAutomationHelper.AddFolderIfNotExists(proj, folderName);
			var ramlItem = folderItem.ProjectItems.Cast<ProjectItem>().First(i => i.Name == ramlFileName);
			var generatedFolderPath = Path.GetDirectoryName(proj.FullName) + Path.DirectorySeparatorChar + folderName + Path.DirectorySeparatorChar;

			var vsixPath = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;
			var templatesPath = vsixPath + "Templates" + Path.DirectorySeparatorChar + "RAMLWebApi2Scaffolder" + Path.DirectorySeparatorChar;

			// Add / Update model objects
			ReplaceTemplateValues(templatesPath, vsixPath, ObjectTemplateName + ".t4", targetNamespace);
			GenerateCodeFromTemplate(Path.Combine(templatesPath, ObjectTemplateName + ".cs.t4"), ramlItem, 
				"apiObject", model.Objects.Values, generatedFolderPath, folderItem);

			// Add / Update controllers definition
			ReplaceTemplateValues(templatesPath, vsixPath, ControllerDeclarationTemplateName + ".t4", targetNamespace);
			GenerateCodeFromTemplate(Path.Combine(templatesPath, ControllerDeclarationTemplateName + ".cs.t4"), ramlItem,
				"controllerObject", model.Controllers, generatedFolderPath, folderItem, "Controller");

			// Add / Update controllers interface
			ReplaceTemplateValues(templatesPath, vsixPath, ControllerInterfaceTemplateName + ".t4", targetNamespace);
			GenerateCodeFromTemplate(Path.Combine(templatesPath, ControllerInterfaceTemplateName + ".cs.t4"), ramlItem,
				"controllerObject", model.Controllers, generatedFolderPath, folderItem, "Controller", true, "I");

			// Add controllers implementation
			var controllersFolderItem = VisualStudioAutomationHelper.AddFolderIfNotExists(proj, "Controllers");
			var controllersFolderPath = Path.GetDirectoryName(proj.FullName) + Path.DirectorySeparatorChar + "Controllers" + Path.DirectorySeparatorChar;
			ReplaceTemplateValues(templatesPath, vsixPath, ControllerImplementationTemplateName + ".t4", targetNamespace);
			GenerateCodeFromTemplate(Path.Combine(templatesPath, ControllerImplementationTemplateName + ".cs.t4"), controllersFolderItem,
				"controllerObject", model.Controllers, controllersFolderPath, folderItem, "Controller", false);
		}

		private void GenerateCodeFromTemplate<T>(string templatePath, ProjectItem projItem, string parameterName, IEnumerable<T> parameterCollection, 
			string folderPath, ProjectItem folderItem, string suffix = null, bool ovewrite = true, string prefix = null) where T : IHasName
		{

			foreach (var parameter in parameterCollection)
			{
				var generatedFileName = GetGeneratedFileName(suffix, prefix, parameter);

				var result = t4Service.TransformText(templatePath, parameterName, parameter);
				var destinationFile = Path.Combine(folderPath, generatedFileName);
				if(ovewrite || !File.Exists(destinationFile))
					File.WriteAllText(destinationFile, result.Content);

				// add file if it does not exist
				var fileItem = projItem.ProjectItems.Cast<ProjectItem>().FirstOrDefault(i => i.Name == generatedFileName);
				if (fileItem != null) continue;

				if (projItem.Name.EndsWith(".raml"))
				{
					var alreadyIncludedInProj = IsAlreadyIncludedInProject(folderPath, folderItem, generatedFileName, projItem);
					if (!alreadyIncludedInProj)
						projItem.ProjectItems.AddFromFile(destinationFile);
				}
				else
				{
					projItem.ProjectItems.AddFromFile(destinationFile);
				}
			}
		}

		private static bool IsAlreadyIncludedInProject(string folderPath, ProjectItem folderItem, string generatedFileName, ProjectItem fileItem)
		{
			var otherRamlFiles = GetOtherRamlFilesInProject(folderPath, fileItem);
			var alreadyIncludedInProj = false;
			foreach (var ramlFile in otherRamlFiles)
			{
				var fileName = Path.GetFileName(ramlFile);
				var otherRamlFileItem =
					folderItem.ProjectItems.Cast<ProjectItem>().FirstOrDefault(i => i.Name == fileName);

				if (otherRamlFileItem == null) continue;
				var item = otherRamlFileItem.ProjectItems.Cast<ProjectItem>().FirstOrDefault(i => i.Name == generatedFileName);
				alreadyIncludedInProj = alreadyIncludedInProj || (item != null);
			}
			return alreadyIncludedInProj;
		}

		private static IEnumerable<string> GetOtherRamlFilesInProject(string folderPath, ProjectItem fileItem)
		{
			var ramlFiles = Directory.EnumerateFiles(folderPath, "*.raml").ToArray();
			var currentRamlFile = fileItem.FileNames[0];
			var otherRamlFiles =
				ramlFiles.Where(f => !String.Equals(f, currentRamlFile, StringComparison.InvariantCultureIgnoreCase));
			return otherRamlFiles;
		}

		private static string GetGeneratedFileName<T>(string suffix, string prefix, T parameter) where T : IHasName
		{
			var name = parameter.Name;
			if (!string.IsNullOrWhiteSpace(prefix))
				name = prefix + name;
			if (!string.IsNullOrWhiteSpace(suffix))
				name += suffix;

			var generatedFileName = name + ".cs";
			return generatedFileName;
		}

		private void ReplaceTemplateValues(string templatesPath, string vsixPath, string templateFileName, string targetNamespace)
		{
			var sourcePath = Path.Combine(templatesPath, templateFileName);
			var fileContent = File.ReadAllText(sourcePath);
			fileContent = fileContent.Replace("$(binDir)", vsixPath);
			fileContent = fileContent.Replace("$(namespace)", targetNamespace);

			var destPath = Path.Combine(templatesPath, Path.GetFileNameWithoutExtension(sourcePath) + ".cs.t4");
			File.WriteAllText(destPath, fileContent);
		}
	}
}