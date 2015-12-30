using System;
using System.Collections.Generic;
using System.Linq;
using Raml.Parser.Expressions;

namespace Raml.Tools
{
    public class RamlTypesHelper
    {
        public static string GetTypeFromApiObject(ApiObject apiObject)
        {
            if (!apiObject.IsArray)
                return apiObject.Name;

            if (apiObject.Type == null)
                return CollectionTypeHelper.GetCollectionType(apiObject.Name);

            return CollectionTypeHelper.GetCollectionType(apiObject.Type);
        }

        public static Verb GetResourceTypeVerb(Method method, Resource resource, IEnumerable<IDictionary<string, ResourceType>> rootResourceTypes)
        {
            var resourceTypes = rootResourceTypes.First(rt => rt.ContainsKey(resource.GetSingleType()));
            var resourceType = resourceTypes[resource.GetSingleType()];
            Verb verb;
            switch (method.Verb)
            {
                case "get":
                    verb = resourceType.Get;
                    break;
                case "delete":
                    verb = resourceType.Delete;
                    break;
                case "post":
                    verb = resourceType.Post;
                    break;
                case "put":
                    verb = resourceType.Put;
                    break;
                case "patch":
                    verb = resourceType.Patch;
                    break;
                case "options":
                    verb = resourceType.Options;
                    break;
                default:
                    throw new InvalidOperationException("Verb not found " + method.Verb);
            }
            return verb;
        }

    }
}