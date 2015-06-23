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
	    protected readonly IDictionary<string, ApiObject> schemaResponseObjects;
	    protected readonly IDictionary<string, ApiObject> schemaRequestObjects;
	    private readonly IDictionary<string, string> linkKeysWithObjectNames;

	    protected MethodsGeneratorBase(RamlDocument raml, IDictionary<string, ApiObject> schemaResponseObjects,
            IDictionary<string, ApiObject> schemaRequestObjects, IDictionary<string, string> linkKeysWithObjectNames)
        {
            this.raml = raml;
            this.schemaResponseObjects = schemaResponseObjects;
	        this.schemaRequestObjects = schemaRequestObjects;
	        this.linkKeysWithObjectNames = linkKeysWithObjectNames;
        }

	    protected string GetReturnType(string key, Method method, Resource resource, string fullUrl)
		{
			if (!method.Responses.Any(r => r.Body != null && r.Body.Any(b => !string.IsNullOrWhiteSpace(b.Value.Schema))))
				return "string";

			var responses = method.Responses
				.Where(r => r.Body != null && r.Body.Any(b => !string.IsNullOrWhiteSpace(b.Value.Schema)))
				.ToArray();

			var returnType = HandleMultipleSchemaType(responses, resource, method, key, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

			return "string";
		}

		private string HandleMultipleSchemaType(IEnumerable<Response> responses, Resource resource, Method method, string key, string fullUrl)
		{
			var properties = GetProperties(responses, resource, method, key, fullUrl);

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

		private List<Property> GetProperties(IEnumerable<Response> responses, Resource resource, Method method, string key, string fullUrl)
		{
			var properties = new List<Property>();
			foreach (var response in responses)
			{
				AddProperty(resource, method, key, response, properties, fullUrl);
			}
			return properties;
		}

		private void AddProperty(Resource resource, Method method, string key, Response response, ICollection<Property> properties, string fullUrl)
		{
			var mimeType = GeneratorServiceHelper.GetMimeType(response);
			if (mimeType == null)
				return;

			var type = GetReturnTypeFromResponse(method, resource, mimeType, key, response.Code, fullUrl);
			if (string.IsNullOrWhiteSpace(type))
				return;

			var property = new Property
			               {
                               Name = CollectionTypeHelper.GetBaseType(type),
				               Description = response.Description + " " + mimeType.Description,
				               Example = mimeType.Example,
				               Type = type,
				               StatusCode = (HttpStatusCode) Enum.Parse(typeof (HttpStatusCode), response.Code),
                               JSONSchema = mimeType.Schema == null ? null : mimeType.Schema.Replace(Environment.NewLine, "").Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"")
			               };

			properties.Add(property);
		}

		protected string GetComment(Resource resource, BasicInfo method)
		{
			var description = resource.Description;
			if (!string.IsNullOrWhiteSpace(method.Description))
				description += string.IsNullOrWhiteSpace(description) ? method.Description : ". " + method.Description;

			description = ParserHelpers.RemoveNewLines(description);

			if (!string.IsNullOrWhiteSpace(resource.DisplayName))
				description += string.IsNullOrWhiteSpace(description) ? resource.DisplayName : " - " + resource.DisplayName;

			return description;
		}


		private string GetReturnTypeFromResponse(Method method, Resource resource, MimeType mimeType, string key, string responseCode, string fullUrl)
		{
			var returnType = GetNamedReturnType(method, resource, mimeType, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

			returnType = GetReturnTypeFromResourceType(method, resource, key, responseCode, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

		    if (ResponseHasKey(key))
		        return GetReturnTypeFromResponseByKey(key);

			var responseKey = key + ParserHelpers.GetStatusCode(responseCode) + GeneratorServiceBase.ResponseContentSuffix;
            if (ResponseHasKey(responseKey))
                return GetReturnTypeFromResponseByKey(responseKey);

		    if (linkKeysWithObjectNames.ContainsKey(key))
		    {
		        var linkedKey = linkKeysWithObjectNames[key];
                if (ResponseHasKey(linkedKey))
                    return GetReturnTypeFromResponseByKey(linkedKey);
		    }

            if (linkKeysWithObjectNames.ContainsKey(responseKey))
            {
                var linkedKey = linkKeysWithObjectNames[responseKey];
                if (ResponseHasKey(linkedKey))
                    return GetReturnTypeFromResponseByKey(linkedKey);
            }

		    return returnType;
		}

	    private bool ResponseHasKey(string key)
	    {
	        return schemaResponseObjects.ContainsKey(key);
	    }

	    private string GetReturnTypeFromResponseByKey(string key)
	    {
	        return schemaResponseObjects[key].IsArray
	            ? CollectionTypeHelper.GetCollectionType(schemaResponseObjects[key].Name)
	            : schemaResponseObjects[key].Name;
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

		private string GetReturnTypeFromResourceType(Method method, Resource resource, string key, string responseCode, string fullUrl)
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
				returnType = GetReturnTypeFromResponseWithoutCheckingResourceTypes(method, resource, resourceTypeMimeType, key, responseCode, fullUrl);
			}
			return returnType;
		}

		// avois infinite recursion
		private string GetReturnTypeFromResponseWithoutCheckingResourceTypes(Method method, Resource resource, MimeType mimeType, string key, string responseCode, string fullUrl)
		{
			var returnType = GetNamedReturnType(method, resource, mimeType, fullUrl);

			if (!string.IsNullOrWhiteSpace(returnType))
				return returnType;

            if (ResponseHasKey(key))
                return GetReturnTypeFromResponseByKey(key);

            var responseKey = key + ParserHelpers.GetStatusCode(responseCode) + GeneratorServiceBase.ResponseContentSuffix;
            if (ResponseHasKey(responseKey))
                return GetReturnTypeFromResponseByKey(responseKey);

            if (linkKeysWithObjectNames.ContainsKey(key))
            {
                var linkedKey = linkKeysWithObjectNames[key];
                if (ResponseHasKey(linkedKey))
                    return GetReturnTypeFromResponseByKey(linkedKey);
            }

            if (linkKeysWithObjectNames.ContainsKey(responseKey))
            {
                var linkedKey = linkKeysWithObjectNames[responseKey];
                if (ResponseHasKey(linkedKey))
                    return GetReturnTypeFromResponseByKey(linkedKey);
            }

			return returnType;
		}


		private string GetNamedReturnType(Method method, Resource resource, MimeType mimeType, string fullUrl)
		{
			var returnType = string.Empty;
			if (mimeType.Schema.Contains("<<") && mimeType.Schema.Contains(">>"))
			{
			    returnType = GetReturnTypeFromParameter(method, resource, mimeType, fullUrl, returnType);
			}
			else if (!mimeType.Schema.Contains("<") && !mimeType.Schema.Contains("{"))
			{
			    returnType = GetReturnTypeFromName(mimeType, returnType);
			}
		    return returnType;
		}

	    private string GetReturnTypeFromName(MimeType mimeType, string returnType)
	    {
	        var type = mimeType.Schema.ToLowerInvariant();
	        if (schemaResponseObjects.Values.Any(o => o.Name.ToLowerInvariant() == type.ToLowerInvariant()))
	        {
	            var apiObject = schemaResponseObjects.Values.First(o => o.Name.ToLowerInvariant() == type.ToLowerInvariant());
                returnType = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name;
	        }
	        return returnType;
	    }

	    private string GetReturnTypeFromParameter(Method method, Resource resource, MimeType mimeType, string fullUrl, string returnType)
	    {
	        var type = schemaParameterParser.Parse(mimeType.Schema, resource, method, fullUrl);
	        if (schemaResponseObjects.Values.Any(o => o.Name.ToLowerInvariant() == type.ToLowerInvariant()))
	        {
	            var apiObject =
	                schemaResponseObjects.Values.First(
	                    o => o.Name.ToLowerInvariant() == type.ToLowerInvariant());
                returnType = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name;
	        }
	        return returnType;
	    }


	    protected GeneratorParameter GetParameter(string key, Method method, Resource resource, string fullUrl)
		{
			var schema = GetJsonSchemaOrDefault(method.Body);
			if (schema != null)
			{
				var generatorParameter = GetGeneratorParameterWhenNamed(method, resource, schema, fullUrl);
				if (generatorParameter != null) 
					return generatorParameter;
			}

			if (resource.Type != null && resource.Type.Any() && raml.ResourceTypes.Any(rt => rt.ContainsKey(resource.GetResourceType())))
			{
				var verb = GetResourceTypeVerb(method, resource);
				if (verb != null && verb.Body != null && !string.IsNullOrWhiteSpace(verb.Body.Schema))
				{
					var generatorParameter = GetGeneratorParameterWhenNamed(method, resource, verb.Body.Schema, fullUrl);
					if (generatorParameter != null)
						return generatorParameter;
				}
			}

			if (RequestHasKey(key))
				return GetGeneratorParameterByKey(key);

			var requestKey = key + GeneratorServiceBase.RequestContentSuffix;
			if (RequestHasKey(requestKey))
				return GetGeneratorParameterByKey(requestKey);

	        if (linkKeysWithObjectNames.ContainsKey(key))
	        {
                var linkedKey = linkKeysWithObjectNames[key];
                if (RequestHasKey(linkedKey))
                    return GetGeneratorParameterByKey(linkedKey);           
	        }

            if (linkKeysWithObjectNames.ContainsKey(requestKey))
            {
                var linkedKey = linkKeysWithObjectNames[requestKey];
                if (RequestHasKey(linkedKey))
                    return GetGeneratorParameterByKey(linkedKey);
            }

			return new GeneratorParameter {Name = "json", Type = "string"};
		}

	    private GeneratorParameter GetGeneratorParameterByKey(string key)
	    {
	        return new GeneratorParameter
	        {
	            Name = schemaRequestObjects[key].Name.ToLower(),
	            Type = schemaRequestObjects[key].Name,
	            Description = schemaRequestObjects[key].Description
	        };
	    }

	    private bool RequestHasKey(string key)
	    {
	        return schemaRequestObjects.ContainsKey(key);
	    }

	    private GeneratorParameter GetGeneratorParameterWhenNamed(Method method, Resource resource,
			string schema, string fullUrl)
		{
			if (schema.Contains("<<") && schema.Contains(">>"))
			{
				var generatorParameter = GetParameterByParametrizedName(method, resource, schema, fullUrl);
				if (generatorParameter != null)
					return generatorParameter;
			}
			else if (!schema.Contains("<") && !schema.Contains("{"))
			{
				var generatorParameter = GetParameterByName(schema);
				if (generatorParameter != null)
					return generatorParameter;
			}
			return null;
		}

		private GeneratorParameter GetParameterByName(string schema)
		{
			GeneratorParameter generatorParameter = null;

			var type = schema.ToLowerInvariant();
			if (schemaRequestObjects.Values.Any(o => o.Name.ToLowerInvariant() == type))
			{
				var apiObject = schemaRequestObjects.Values.First(o => o.Name.ToLowerInvariant() == type);
				generatorParameter = new GeneratorParameter
				                     {
					                     Name = apiObject.Name.ToLower(),
                                         Type = apiObject.IsArray ? CollectionTypeHelper.GetCollectionType(apiObject.Name) : apiObject.Name,
					                     Description = apiObject.Description
				                     };
			}
			return generatorParameter;
		}

		private GeneratorParameter GetParameterByParametrizedName(Method method, Resource resource, string schema, string fullUrl)
		{
			GeneratorParameter generatorParameter = null;
			var type = schemaParameterParser.Parse(schema, resource, method, fullUrl);
			if (schemaRequestObjects.Values.Any(o => o.Name.ToLower() == type.ToLowerInvariant()))
			{
				var apiObject = schemaRequestObjects.Values.First(o => o.Name.ToLower() == type.ToLowerInvariant());
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

		protected bool IsVerbForMethod(Method method)
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