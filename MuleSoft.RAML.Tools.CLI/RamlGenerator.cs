using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Raml.Common;
using Raml.Parser;
using Raml.Parser.Expressions;
using Task = System.Threading.Tasks.Task;

namespace MuleSoft.RAML.Tools.CLI
{
    public class RamlGenerator
    {
        public async Task HandleReference(Options opts)
        {
            string destinationFolder;
            string templatesFolder;
            string targetFileName;
            string targetNamespace;
            HandleParameters(opts, out destinationFolder, out templatesFolder, out targetFileName, out targetNamespace);

            var ramlDoc = await GetRamlDocument(opts, destinationFolder, targetFileName);

            var generator = new RamlClientGenerator();
            generator.Generate(ramlDoc, targetFileName, targetNamespace, templatesFolder, destinationFolder);
        }


        private static async Task<RamlDocument> GetRamlDocument(Options opts, string destinationFolder, string targetFileName)
        {
            var result = new RamlIncludesManager().Manage(opts.Source, destinationFolder, targetFileName, opts.Overwrite);

            var path = Path.Combine(destinationFolder, targetFileName);
            File.WriteAllText(path, result.ModifiedContents);
            var parser = new RamlParser();
            var ramlDoc = await parser.LoadAsync(path);
            return ramlDoc;
        }

        private void HandleParameters(Options opts, out string destinationFolder, out string templatesFolder,
            out string targetFileName, out string targetNamespace)
        {
            destinationFolder = opts.DestinationFolder ?? "generated";
            templatesFolder = string.IsNullOrWhiteSpace(opts.TemplatesFolder)
                ? Path.GetDirectoryName(Assembly.GetAssembly(typeof (Program)).Location)
                : opts.TemplatesFolder;

            targetFileName = Path.GetFileName(opts.Source);
            if (string.IsNullOrWhiteSpace(targetFileName))
                targetFileName = "root.raml";

            if (!targetFileName.EndsWith(".raml"))
                targetFileName += ".raml";

            targetNamespace = string.IsNullOrWhiteSpace(opts.Namespace)
                ? NetNamingMapper.GetNamespace(Path.GetFileNameWithoutExtension(targetFileName))
                : opts.Namespace;
        }

        public async Task HandleContract(ContractOptions opts)
        {
            string destinationFolder;
            string templatesFolder;
            string targetFileName;
            string targetNamespace;
            HandleParameters(opts, out destinationFolder, out templatesFolder, out targetFileName, out targetNamespace);

            var ramlDoc = await GetRamlDocument(opts, destinationFolder, targetFileName);

            var generator = new RamlWebApiGenerator(ramlDoc, targetNamespace, templatesFolder, targetFileName, destinationFolder, opts.UseAsyncMethods);
            generator.Generate();
        }


    }
}