using System;
using System.Collections.Generic;
using System.Linq;
using Raml.Parser.Expressions;

namespace Raml.Tools
{
    public class RamlTypesHelper
    {
        public static string DecodeRaml1Type(string type)
        {
            // TODO: can I handle this better ?
            if (type.Contains("(") || type.Contains("|"))
                return "object";

            if (type.EndsWith("[][]")) // array of arrays
                return CollectionTypeHelper.GetCollectionType(CollectionTypeHelper.GetCollectionType(type.Substring(0, type.Length - 4)));

            if (type.EndsWith("[]")) // array
                return CollectionTypeHelper.GetCollectionType(type.Substring(0, type.Length - 2));

            if (type.EndsWith("{}")) // Map
            {
                var subtype = type.Substring(0, type.Length - 2);
                var netType = NetTypeMapper.Map(subtype);
                if (!string.IsNullOrWhiteSpace(netType))
                    return "IDictionary<string, " + netType + ">";

               return "IDictionary<string, " + subtype + ">";
            }

            return type;
        }

        public static string GetTypeFromApiObject(ApiObject apiObject)
        {
            if (apiObject.IsArray)
            {
                if (string.IsNullOrWhiteSpace(apiObject.Type))
                    return CollectionTypeHelper.GetCollectionType(apiObject.Name);

                return CollectionTypeHelper.GetCollectionType(apiObject.Type);
            }
            if (apiObject.IsMap)
                return apiObject.Type;

            if (!string.IsNullOrWhiteSpace(apiObject.Type) && apiObject.Type != apiObject.Name)
                return apiObject.Type;

            return apiObject.Name;
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