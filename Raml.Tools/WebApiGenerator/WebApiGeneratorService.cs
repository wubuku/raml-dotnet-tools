using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Raml.Common;
using Raml.Parser.Expressions;

namespace Raml.Tools.WebApiGenerator
{
    public class WebApiGeneratorService : GeneratorServiceBase
    {
        private WebApiMethodsGenerator webApiMethodsGenerator;
        public WebApiGeneratorService(RamlDocument raml) : base(raml)
        {
        }

        public WebApiGeneratorModel BuildModel()
        {
            classesNames = new Collection<string>();
            warnings = new Dictionary<string, string>();
            enums = new Dictionary<string, ApiEnum>();

            schemaRequestObjects = GetRequestObjects();
            schemaResponseObjects = GetResponseObjects();

            CleanProperties(schemaRequestObjects);
            CleanProperties(schemaResponseObjects);

            webApiMethodsGenerator = new WebApiMethodsGenerator(raml, schemaResponseObjects, schemaRequestObjects, linkKeysWithObjectNames);

            var controllers = GetControllers().ToArray();

            CleanNotUsedObjects(controllers);

            return new WebApiGeneratorModel
                   {
                       Namespace = NetNamingMapper.GetNamespace(raml.Title),
                       Controllers = controllers,
                       RequestObjects = schemaRequestObjects,
                       ResponseObjects = schemaResponseObjects,
                       Warnings = warnings,
                       Enums = Enums
                   };
        }

        private void CleanNotUsedObjects(IEnumerable<ControllerObject> controllers)
        {
            apiObjectsCleaner.CleanObjects(controllers, schemaRequestObjects, apiObjectsCleaner.IsUsedAsParameterInAnyMethod);

            apiObjectsCleaner.CleanObjects(controllers, schemaResponseObjects, apiObjectsCleaner.IsUsedAsResponseInAnyMethod);
        }

        private IEnumerable<ControllerObject> GetControllers()
        {
            var classes = new List<ControllerObject>();
            var classesObjectsRegistry = new Dictionary<string, ControllerObject>();

            GetControllers(classes, classesNames, classesObjectsRegistry);

            return classes;
        }

        private void GetControllers(IList<ControllerObject> classes, 
            ICollection<string> classesNames, IDictionary<string, ControllerObject> classesObjectsRegistry)
        {
            var rootController = new ControllerObject { Name = "Home", PrefixUri = "/" };

            foreach (var resource in raml.Resources)
            {
                if (resource == null)
                    continue;

                var fullUrl = GetUrl(string.Empty, resource.RelativeUri);

                // when the resource is a parameter dont generate a class but add it's methods and children to the parent
                if (resource.RelativeUri.StartsWith("/{") && resource.RelativeUri.EndsWith("}"))
                {
                    var generatedMethods = webApiMethodsGenerator.GetMethods(resource, fullUrl, rootController, rootController.Name);
                    foreach (var method in generatedMethods)
                    {
                        rootController.Methods.Add(method);
                    }

                    GetMethodsFromChildResources(resource.Resources, fullUrl, rootController);

                    classesNames.Add(rootController.Name);
                    classesObjectsRegistry.Add(CalculateClassKey("/"), rootController);
                    classes.Add(rootController);
                }
                else
                {
                    var controller = new ControllerObject
                    {
                        Name = GetUniqueObjectName(resource, null),
                        PrefixUri = UrlGeneratorHelper.FixControllerRoutePrefix(fullUrl),
                        Description = resource.Description,
                    };

                    var methods = webApiMethodsGenerator.GetMethods(resource, fullUrl, controller, controller.Name);
                    foreach (var method in methods)
                    {
                        controller.Methods.Add(method);
                    }

                    classesNames.Add(controller.Name);
                    classes.Add(controller);
                    classesObjectsRegistry.Add(CalculateClassKey(fullUrl), controller);

                    GetMethodsFromChildResources(resource.Resources, fullUrl, controller);
                }
            }
        }


        private void GetMethodsFromChildResources(IEnumerable<Resource> resources, string url, ControllerObject parentController)
        {
            if (resources == null)
                return;

            foreach (var resource in resources)
            {
                if (resource == null)
                    continue;

                var fullUrl = GetUrl(url, resource.RelativeUri);

                var methods = webApiMethodsGenerator.GetMethods(resource, fullUrl, parentController, parentController.Name);
                foreach (var method in methods)
                {
                    parentController.Methods.Add(method);
                }

                GetMethodsFromChildResources(resource.Resources, fullUrl, parentController);
            }
        }




    }
}