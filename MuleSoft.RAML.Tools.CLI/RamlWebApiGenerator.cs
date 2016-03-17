using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TextTemplating;
using Raml.Parser.Expressions;
using Raml.Tools;
using Raml.Tools.WebApiGenerator;

namespace MuleSoft.RAML.Tools.CLI
{
    public class RamlWebApiGenerator
    {
        private readonly string modelT4Template = "ApiModel.t4";
        private readonly string enumT4Template = "ApiEnum.t4";
        private readonly string baseControllerT4Template = "ApiControllerBase.t4";
        private readonly string implementationControllerT4Template = "ApiControllerImplementation.t4";
        private readonly string interfaceControllerT4Template = "ApiControllerInterface.t4";

        private readonly RamlDocument ramlDoc;
        private readonly string targetNamespace;
        private readonly string templatesFolder;
        private readonly string targetFileName;
        private readonly string destinationFolder;
        private readonly CustomCmdLineHost host;
        private readonly Engine engine;
        private readonly bool useAsyncMethods;
        private bool hasModels;

        public RamlWebApiGenerator(RamlDocument ramlDoc, string targetNamespace, string templatesFolder, 
            string targetFileName, string destinationFolder, bool useAsyncMethods)
        {
            this.ramlDoc = ramlDoc;
            this.targetNamespace = targetNamespace;
            this.templatesFolder = templatesFolder;
            this.targetFileName = targetFileName;
            this.destinationFolder = destinationFolder;
            this.useAsyncMethods = useAsyncMethods;
            host = new CustomCmdLineHost();
            engine = new Engine();
        }

        public void Generate()
        {
            var model = new WebApiGeneratorService(ramlDoc, targetNamespace).BuildModel();

            var models = model.Objects;
            // when is an XML model, skip empty objects
            if (models.Any(o => !string.IsNullOrWhiteSpace(o.GeneratedCode)))
                models = models.Where(o => o.Properties.Any() || !string.IsNullOrWhiteSpace(o.GeneratedCode));

            models = models.Where(o => !o.IsArray || o.Type == null); // skip array of primitives

            hasModels = models.Any();

            GenerateModels(model.Objects);
            GenerateEnums(model.Enums);
            GenerateInterfaceControllers(model.Controllers);
            GenerateBaseControllers(model.Controllers);
            GenerateImplementationControllers(model.Controllers);
        }

        private void GenerateImplementationControllers(IEnumerable<ControllerObject> controllers)
        {
            GenerateModels(controllers, "controllerObject", implementationControllerT4Template);
        }

        private void GenerateBaseControllers(IEnumerable<ControllerObject> controllers)
        {
            var extraParams = new Dictionary<string, bool>
            {
                {"useAsyncMethods", useAsyncMethods},
                {"hasModels", hasModels}
            };
            GenerateModels(controllers, "controllerObject", baseControllerT4Template, extraParams);
        }

        private void GenerateInterfaceControllers(IEnumerable<ControllerObject> controllers)
        {
            GenerateModels(controllers, "controllerObject", interfaceControllerT4Template);
        }

        private void GenerateEnums(IEnumerable<ApiEnum> enums)
        {
            GenerateModels(enums, "apiEnum", enumT4Template);
        }

        private void GenerateModels(IEnumerable<ApiObject> models)
        {
            GenerateModels(models,"apiObject", modelT4Template);
        }

        private void GenerateModels<T>(IEnumerable<T> models, string parameterKey, string t4TemplateName, IDictionary<string, bool> extraParams = null) where T : IHasName
        {
            foreach (var parameter in models)
            {
                var result = GenerateModel(parameter, t4TemplateName, parameterKey, extraParams);

                foreach (CompilerError error in result.Errors)
                {
                    Console.WriteLine(error.ToString());
                }

                if (result.Errors.Count == 0)
                    File.WriteAllText(Path.Combine(destinationFolder, parameter.Name + ".cs"), result.Content, result.Encoding);
            }
        }

        private GenerationResult GenerateModel<T>(T parameter, string parameterKey, string t4TemplateName, IDictionary<string, bool> extraParams = null) where T : IHasName

        {
            host.TemplateFileValue = Path.Combine(templatesFolder, t4TemplateName);
            var extensionPath = Path.GetDirectoryName(GetType().Assembly.Location) + Path.DirectorySeparatorChar;

            // Read the T4 from disk into memory
            var templateFileContent = File.ReadAllText(Path.Combine(templatesFolder, t4TemplateName));
            templateFileContent = templateFileContent.Replace("$(binDir)", extensionPath);
            templateFileContent = templateFileContent.Replace("$(ramlFile)", targetFileName.Replace("\\", "\\\\"));
            templateFileContent = templateFileContent.Replace("$(namespace)", targetNamespace);

            host.Session = host.CreateSession();
            host.Session[parameterKey] = parameter;

            if (extraParams != null)
            {
                foreach (var extraParam in extraParams)
                {
                    host.Session[extraParam.Key] = extraParam.Value;
                }
            }

            var output = engine.ProcessTemplate(templateFileContent, host);

            return new GenerationResult
            {
                Content = output,
                Encoding = host.FileEncoding,
                Errors = host.Errors
            };
        }
    }
}