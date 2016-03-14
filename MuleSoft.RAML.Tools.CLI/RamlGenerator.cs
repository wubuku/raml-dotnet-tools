using System.IO;
using System.Reflection;
using Microsoft.VisualStudio.Shell;
using Raml.Common;
using Raml.Parser;
using Raml.Tools.ClientGenerator;
using Task = System.Threading.Tasks.Task;

namespace MuleSoft.RAML.Tools.CLI
{
    public class RamlGenerator
    {
        private static string ClientT4TemplateName;

        public async Task HandleReference(ReferenceOptions opts)
        {
            var ramlFilePath = Path.GetTempPath();
            var ramlTitle = string.IsNullOrWhiteSpace(opts.Title) ? Path.GetFileName(opts.Source) : opts.Title;
            var templatesPath = Assembly.GetAssembly(typeof(Program)).Location;
            var targetFileName = string.IsNullOrWhiteSpace(opts.File)
                ? Path.GetFileNameWithoutExtension(ramlFilePath)
                : opts.File;

            if (opts.Add)
            {
            }
        }

        public async Task HandleContract(ContractOptions opts)
        {
            var ramlFilePath = Path.GetTempPath();
            var ramlTitle = string.IsNullOrWhiteSpace(opts.Title) ? Path.GetFileName(opts.Source) : opts.Title;
            var templatesPath = Assembly.GetAssembly(typeof(Program)).Location;
            var targetFileName = string.IsNullOrWhiteSpace(opts.File)
                ? Path.GetFileNameWithoutExtension(ramlFilePath)
                : opts.File;
            var targetNamespace = opts.Namespace;

            if (opts.Add)
            {
                var parser = new RamlParser();
                bool overwrite = false;
                string destinationFolder = templatesPath;
                var result = new RamlIncludesManager().Manage(opts.Source, destinationFolder, targetFileName, overwrite);
                var ramlDoc = await parser.LoadRamlAsync(result.ModifiedContents, destinationFolder);
                var model = new ClientGeneratorService(ramlDoc, "clientRootClassName", targetNamespace).BuildModel();
                var templateFolder = templatesPath;
                var templateFilePath = Path.Combine(templateFolder, ClientT4TemplateName);
                var extensionPath = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;
                var t4Service = new T4Service(ServiceProvider.GlobalProvider);
                var res = t4Service.TransformText(templateFilePath, model, extensionPath, opts.Source, targetNamespace);
            }
        }

         
    }
}