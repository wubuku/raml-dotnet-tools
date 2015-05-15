using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Raml.Common;
using Raml.Parser.Expressions;
using Raml.Tools.Pluralization;

namespace Raml.Tools
{
	public abstract class MethodsGeneratorBase
	{
		protected readonly string[] suffixes = { "A", "B", "C", "D", "E", "F", "G" };
		protected readonly SchemaParameterParser schemaParameterParser = new SchemaParameterParser(new EnglishPluralizationService());
		protected readonly UriParametersGenerator uriParametersGenerator = new UriParametersGenerator();

		protected readonly RamlDocument raml;

		protected MethodsGeneratorBase(RamlDocument raml)
		{
			this.raml = raml;
		}

		protected string GetReturnType(string key, Method method, Resource resource, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			if (!method.Responses.Any(r => r.Body != null && r.Body.Any(b => !string.IsNullOrWhiteSpace(b.Value.Schema))))
				return "string";

			var responses = method.Responses
				.Where(r => r.Body != null && r.Body.Any(b => !string.IsNullOrWhiteSpace(b.Value.Schema)))
				.ToArray();

			var returnType = HandleMultipleSchemaType(responses, resource, method, key, schemaResponseObjects, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

			return "string";
		}

		private string HandleMultipleSchemaType(IEnumerable<Response> responses, Resource resource, Method method, string key, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			var properties = GetProperties(responses, resource, method, key, schemaResponseObjects, fullUrl);

			if (properties.Count == 0)
				return "string";

			if (properties.Count == 1)
				return properties.First().Type;

			// Build a new response object containing all types
			var name = NetNamingMapper.GetObjectName("Multiple" + key);
			var apiObject = new ApiObject
			{
				Name = name,
				Description = "Multiple Response Types " + string.Join(", ", properties.Select(p => p.Name)),
				Properties = properties,
				IsMultiple = true
			};
			schemaResponseObjects.Add(new KeyValuePair<string, ApiObject>(name, apiObject));
			return name;
		}

		private List<Property> GetProperties(IEnumerable<Response> responses, Resource resource, Method method, string key, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			var properties = new List<Property>();
			foreach (var response in responses)
			{
				AddProperty(resource, method, key, schemaResponseObjects, response, properties, fullUrl);
			}
			return properties;
		}

		private void AddProperty(Resource resource, Method method, string key, IDictionary<string, ApiObject> schemaResponseObjects,
			Response response, List<Property> properties, string fullUrl)
		{
			var mimeType = GeneratorServiceHelper.GetMimeType(response);
			if (mimeType == null)
				return;

			var type = GetReturnTypeFromResponse(method, resource, mimeType, key, response.Code, schemaResponseObjects, fullUrl);
			if (string.IsNullOrWhiteSpace(type))
				return;

			var property = new Property
			               {
                               Name = CollectionTypeHelper.GetBaseType(type),
				               Description = response.Description + " " + mimeType.Description,
				               Example = mimeType.Example,
				               Type = type,
				               StatusCode = (HttpStatusCode) Enum.Parse(typeof (HttpStatusCode), response.Code),
                               JSONSchema = mimeType.Schema == null ? null : mimeType.Schema.Replace("\"", "\\\"").Replace("\n", "")
			               };

			properties.Add(property);
		}

		protected static string GetComment(Resource resource, BasicInfo method)
		{
			var description = resource.Description;
			if (!string.IsNullOrWhiteSpace(method.Description))
				description += string.IsNullOrWhiteSpace(description) ? method.Description : ". " + method.Description;

			description = ParserHelpers.RemoveNewLines(description);

			if (!string.IsNullOrWhiteSpace(resource.DisplayName))
				description += string.IsNullOrWhiteSpace(description) ? resource.DisplayName : " - " + resource.DisplayName;

			return description;
		}


		private string GetReturnTypeFromResponse(Method method, Resource resource, MimeType mimeType, string key, string responseCode, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			var returnType = GetNamedReturnType(method, resource, mimeType, schemaResponseObjects, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

			returnType = GetReturnTypeFromResourceType(method, resource, key, responseCode, schemaResponseObjects, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

		    if (schemaResponseObjects.ContainsKey(key) && schemaResponseObjects[key].Properties.Any())
		        return schemaResponseObjects[key].IsArray
		            ? CollectionTypeHelper.GetCollectionType(schemaResponseObjects[key].Name)
		            : schemaResponseObjects[key].Name;

			var responseKey = key + ParserHelpers.GetStatusCode(responseCode) + GeneratorServiceBase.ResponseContentSuffix;
		    if (schemaResponseObjects.ContainsKey(responseKey) && schemaResponseObjects[responseKey].Properties.Any())
		        return schemaResponseObjects[responseKey].IsArray
		            ? CollectionTypeHelper.GetCollectionType(schemaResponseObjects[responseKey].Name)
		            : schemaResponseObjects[responseKey].Name;

			return returnType;
		}

		protected Verb GetResourceTypeVerb(Method method, Resource resource)
		{
			var resourceTypes = raml.ResourceTypes.First(rt => rt.ContainsKey(resource.GetResourceType()));
			var resourceType = resourceTypes[resource.GetResourceType()];
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

		private string GetReturnTypeFromResourceType(Method method, Resource resource, string key, string responseCode, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			var returnType = string.Empty;
			if (resource.Type == null || !resource.Type.Any() ||
				!raml.ResourceTypes.Any(rt => rt.ContainsKey(resource.GetResourceType())))
				return returnType;

			var verb = GetResourceTypeVerb(method, resource);
			if (verb == null || verb.Responses == null
				|| !verb.Responses.Any(r => r != null && r.Body != null
											&& r.Body.Values.Any(m => !string.IsNullOrWhiteSpace(m.Schema))))
				return returnType;

			var response = verb.Responses.FirstOrDefault(r => r.Code == responseCode);
			if (response == null)
				return returnType;

			var resourceTypeMimeType = GeneratorServiceHelper.GetMimeType(response);
			if (resourceTypeMimeType != null)
			{
				returnType = GetReturnTypeFromResponseWithoutCheckingResourceTypes(method, resource, resourceTypeMimeType, key, responseCode, schemaResponseObjects, fullUrl);
			}
			return returnType;
		}

		// avois infinite recursion
		private string GetReturnTypeFromResponseWithoutCheckingResourceTypes(Method method, Resource resource, MimeType mimeType, string key, string responseCode, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			var returnType = GetNamedReturnType(method, resource, mimeType, schemaResponseObjects, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

		    if (schemaResponseObjects.ContainsKey(key) && schemaResponseObjects[key].Properties.Any())
		        return schemaResponseObjects[key].IsArray
		            ? CollectionTypeHelper.GetCollectionType(schemaResponseObjects[key].Name)
		            : schemaResponseObjects[key].Name;

			var responseKey = key + ParserHelpers.GetStatusCode(responseCode) + GeneratorServiceBase.ResponseContentSuffix;
		    if (schemaResponseObjects.ContainsKey(responseKey) && schemaResponseObjects[responseKey].Properties.Any())
		        return schemaResponseObjects[responseKey].IsArray
		            ? CollectionTypeHelper.GetCollectionType(schemaResponseObjects[responseKey].Name)
		            : schemaResponseObjects[responseKey].Name;

			return returnType;
		}


		private string GetNamedReturnType(Method method, Resource resource, MimeType mimeType, IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl)
		{
			var returnType = string.Empty;
			if (mimeType.Schema.Contains("<<") && mimeType.Schema.Contains(">>"))
			{
			    returnType = GetReturnTypeFromParameter(method, resource, mimeType, schemaResponseObjects, fullUrl, returnType);
			}
			else if (!mimeType.Schema.Contains("<") && !mimeType.Schema.Contains("{"))
			{
			    returnType = GetReturnTypeFromName(mimeType, schemaResponseObjects, returnType);
			}
		    return returnType;
		}

	    private static string GetReturnTypeFromName(MimeType mimeType, IDictionary<string, ApiObject> schemaResponseObjects, string returnType)
	    {
	        var type = mimeType.Schema.ToLowerInvariant();
	        if (schemaResponseObjects.Values.Any(o => o.Properties.Any() && o.Name.ToLowerInvariant() == type.ToLowerInvariant()))
	        {
	            var apiObject = schemaResponseObjects.Values.First(o => o.Properties.Any() && o.Name.ToLowerInvariant() == type.ToLowerInvariant());
                returnType = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name;
	        }
	        return returnType;
	    }

	    private string GetReturnTypeFromParameter(Method method, Resource resource, MimeType mimeType,
	        IDictionary<string, ApiObject> schemaResponseObjects, string fullUrl, string returnType)
	    {
	        var type = schemaParameterParser.Parse(mimeType.Schema, resource, method, fullUrl);
	        if (schemaResponseObjects.Values.Any(o => o.Properties.Any() && o.Name.ToLowerInvariant() == type.ToLowerInvariant()))
	        {
	            var apiObject =
	                schemaResponseObjects.Values.First(
	                    o => o.Properties.Any() && o.Name.ToLowerInvariant() == type.ToLowerInvariant());
                returnType = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name;
	        }
	        return returnType;
	    }


	    protected GeneratorParameter GetParameter(string key, Method method, Resource resource, IDictionary<string, ApiObject> schemaRequestObjects, string fullUrl)
		{
			var schema = GetJsonSchemaOrDefault(method.Body);
			if (schema != null)
			{
				var generatorParameter = GetGeneratorParameterWhenNamed(method, resource, schemaRequestObjects, schema, fullUrl);
				if (generatorParameter != null) 
					return generatorParameter;
			}

			if (resource.Type != null && resource.Type.Any() && raml.ResourceTypes.Any(rt => rt.ContainsKey(resource.GetResourceType())))
			{
				var verb = GetResourceTypeVerb(method, resource);
				if (verb != null && verb.Body != null && !string.IsNullOrWhiteSpace(verb.Body.Schema))
				{
					var generatorParameter = GetGeneratorParameterWhenNamed(method, resource, schemaRequestObjects, verb.Body.Schema, fullUrl);
					if (generatorParameter != null)
						return generatorParameter;
				}
			}

			if (schemaRequestObjects.ContainsKey(key) && schemaRequestObjects[key].Properties.Any())
				return new GeneratorParameter
				       {
					       Name = schemaRequestObjects[key].Name.ToLower(),
					       Type = schemaRequestObjects[key].Name,
					       Description = schemaRequestObjects[key].Description
				       };

			var requestKey = key + GeneratorServiceBase.RequestContentSuffix;
			if (schemaRequestObjects.ContainsKey(requestKey) && schemaRequestObjects[requestKey].Properties.Any())
				return new GeneratorParameter
				       {
					       Name = schemaRequestObjects[requestKey].Name.ToLower(),
					       Type = schemaRequestObjects[requestKey].Name,
					       Description = schemaRequestObjects[requestKey].Description
				       };

			return new GeneratorParameter {Name = "json", Type = "string"};
		}

		private GeneratorParameter GetGeneratorParameterWhenNamed(Method method, Resource resource, IDictionary<string, ApiObject> schemaRequestObjects,
			string schema, string fullUrl)
		{
			if (schema.Contains("<<") && schema.Contains(">>"))
			{
				var generatorParameter = GetParameterByParametrizedName(method, resource, schemaRequestObjects, schema, fullUrl);
				if (generatorParameter != null)
					return generatorParameter;
			}
			else if (!schema.Contains("<") && !schema.Contains("{"))
			{
				var generatorParameter = GetParameterByName(schemaRequestObjects, schema);
				if (generatorParameter != null)
					return generatorParameter;
			}
			return null;
		}

		private static GeneratorParameter GetParameterByName(IDictionary<string, ApiObject> schemaRequestObjects, string schema)
		{
			GeneratorParameter generatorParameter = null;

			var type = schema.ToLowerInvariant();
			if (schemaRequestObjects.Values.Any(o => o.Properties.Any() && o.Name.ToLowerInvariant() == type))
			{
				var apiObject = schemaRequestObjects.Values.First(o => o.Properties.Any() && o.Name.ToLowerInvariant() == type);
				generatorParameter = new GeneratorParameter
				                     {
					                     Name = apiObject.Name.ToLower(),
                                         Type = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name,
					                     Description = apiObject.Description
				                     };
			}
			return generatorParameter;
		}

		private GeneratorParameter GetParameterByParametrizedName(Method method, Resource resource,
			IDictionary<string, ApiObject> schemaRequestObjects, string schema, string fullUrl)
		{
			GeneratorParameter generatorParameter = null;
			var type = schemaParameterParser.Parse(schema, resource, method, fullUrl);
			if (schemaRequestObjects.Values.Any(o => o.Properties.Any() && o.Name.ToLower() == type.ToLowerInvariant()))
			{
				var apiObject = schemaRequestObjects.Values.First(o => o.Properties.Any() && o.Name.ToLower() == type.ToLowerInvariant());
				generatorParameter = new GeneratorParameter
				                     {
					                     Name = apiObject.Name.ToLower(),
                                         Type = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name,
					                     Description = apiObject.Description
				                     };
			}
			return generatorParameter;
		}

		private string GetJsonSchemaOrDefault(IDictionary<string, MimeType> body)
		{
			if (body.Any(b => b.Key.ToLowerInvariant().Contains("json") && !string.IsNullOrWhiteSpace(b.Value.Schema)))
				return body.First(b => b.Key.ToLowerInvariant().Contains("json") && !string.IsNullOrWhiteSpace(b.Value.Schema)).Value.Schema;

			var isDefaultMediaTypeDefined = !string.IsNullOrWhiteSpace(raml.MediaType);
			var hasSchemaWithDefaultMediaType = raml.MediaType != null &&
			                                    body.Any(b => b.Key.ToLowerInvariant() == raml.MediaType.ToLowerInvariant()
			                                                  && !string.IsNullOrWhiteSpace(b.Value.Schema));

			if (isDefaultMediaTypeDefined && hasSchemaWithDefaultMediaType)
				return body.First(b => b.Key.ToLowerInvariant() == raml.MediaType.ToLowerInvariant() && !string.IsNullOrWhiteSpace(b.Value.Schema)).Value.Schema;

			if (body.Any(b => !string.IsNullOrWhiteSpace(b.Value.Schema)))
				return body.First(b => !string.IsNullOrWhiteSpace(b.Value.Schema)).Value.Schema;

			return null;
		}

		protected static bool IsVerbForMethod(Method method)
		{
			if (method.Verb == null)
				return true;

			return method.Verb.ToLower() != "options" && method.Verb.ToLower() != "head" && method.Verb.ToLower() != "trace" && method.Verb.ToLower() != "connect" && method.Verb.ToLower() != "patch";
		}

		protected string GetUniqueName(ICollection<string> methodsNames, string methodName, string relativeUri)
		{
			var nameWithResource = NetNamingMapper.GetMethodName(methodName + relativeUri);
			if (!methodsNames.Contains(nameWithResource))
				return nameWithResource;

			for (var i = 0; i < 7; i++)
			{
				var unique = methodName + suffixes[i];
				if (!methodsNames.Contains(unique))
					return unique;
			}
			for (var i = 0; i < 100; i++)
			{
				var unique = methodName + i;
				if (!methodsNames.Contains(unique))
					return unique;
			}
			throw new InvalidOperationException("Could not find a unique name for method " + methodName);
		}

	}
}