using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using Raml.Parser.Expressions;

namespace Raml.Tools.ClientGenerator
{
	public class ClientMethodsGenerator : MethodsGeneratorBase
	{
		private readonly string DefaultHeaderType = typeof(HttpResponseHeaders).Name;

		public ClientMethodsGenerator(RamlDocument raml) : base(raml)
		{
		}

		public ICollection<ClientGeneratorMethod> GetMethods(Resource resource, string url, ClassObject parent, string objectName, IDictionary<string, ApiObject> schemaResponseObjects, IDictionary<string, ApiObject> uriParameterObjects, IDictionary<string, ApiObject> queryObjects, IDictionary<string, ApiObject> headerObjects, IDictionary<string, ApiObject> responseHeadersObjects, IDictionary<string, ApiObject> schemaRequestObjects)
		{
			var methodsNames = new List<string>();
			if (parent != null)
				methodsNames = parent.Methods.Select(m => m.Name).ToList();

			var generatorMethods = new Collection<ClientGeneratorMethod>();
			if (resource.Methods == null)
				return generatorMethods;

			foreach (var method in resource.Methods)
			{
				AddGeneratedMethod(resource, url, objectName, schemaResponseObjects, uriParameterObjects, queryObjects,
					headerObjects, responseHeadersObjects, method, methodsNames, generatorMethods, schemaRequestObjects);
			}

			return generatorMethods;
		}

		private void AddGeneratedMethod(Resource resource, string url, string objectName, IDictionary<string, ApiObject> schemaResponseObjects,
			IDictionary<string, ApiObject> uriParameterObjects, IDictionary<string, ApiObject> queryObjects, IDictionary<string, ApiObject> headerObjects,
			IDictionary<string, ApiObject> responseHeadersObjects, Method method, List<string> methodsNames, ICollection<ClientGeneratorMethod> generatorMethods, IDictionary<string, ApiObject> schemaRequestObjects)
		{
			var generatedMethod = BuildClassMethod(url, method, resource, schemaRequestObjects, schemaResponseObjects);
			if (generatedMethod.ReturnType != "string")
			{
				generatedMethod.ReturnTypeObject = schemaResponseObjects.Values
					.First(o => o.Name == generatedMethod.ReturnType.Replace("[]", ""));

				generatedMethod.OkReturnType = GetOkReturnType(generatedMethod);
			}
			uriParametersGenerator.Generate(resource, url, generatedMethod, uriParameterObjects);

			if (!IsVerbForMethod(method)) return;

			if (methodsNames.Contains(generatedMethod.Name))
				generatedMethod.Name = GetUniqueName(methodsNames, generatedMethod.Name, resource.RelativeUri);

			GetQueryParameters(objectName, method, generatedMethod, queryObjects);

			GetHeaders(objectName, method, generatedMethod, headerObjects);

			GetResponseHeaders(objectName, generatedMethod, method, responseHeadersObjects);

			generatorMethods.Add(generatedMethod);
			methodsNames.Add(generatedMethod.Name);
		}

		private ClientGeneratorMethod BuildClassMethod(string url, Method method, Resource resource, IDictionary<string, ApiObject> schemaRequestObjects, IDictionary<string, ApiObject> schemaResponseObjects)
		{
			var generatedMethod = new ClientGeneratorMethod
			{
				Name = NetNamingMapper.GetMethodName(method.Verb ?? "Get" + resource.RelativeUri),
				ReturnType = GetReturnType(GeneratorServiceHelper.GetKeyForResource(method, resource), method, resource, schemaResponseObjects, url),
				Parameter = GetParameter(GeneratorServiceHelper.GetKeyForResource(method, resource), method, resource, schemaRequestObjects, url),
				Comment = GetComment(resource, method),
				Url = url,
				Verb = NetNamingMapper.Capitalize(method.Verb),
				Parent = null,
				UseSecurity =
					raml.SecuredBy != null && raml.SecuredBy.Any() ||
					resource.Methods.Any(m => m.Verb == method.Verb && m.SecuredBy != null && m.SecuredBy.Any())
			};
			return generatedMethod;
		}


		private static string GetOkReturnType(ClientGeneratorMethod generatedMethod)
		{
			if (!generatedMethod.ReturnTypeObject.IsMultiple)
				return generatedMethod.ReturnType;

			if (generatedMethod.ReturnTypeObject.Properties.Any(p => p.StatusCode == HttpStatusCode.OK))
				return generatedMethod.ReturnTypeObject.Properties.First(p => p.StatusCode == HttpStatusCode.OK).Type;

			return generatedMethod.ReturnTypeObject.Properties.First().Type;
		}

		private void GetQueryParameters(string objectName, Method method, ClientGeneratorMethod generatedMethod, IDictionary<string, ApiObject> queryObjects)
		{
			if (method.QueryParameters != null && method.QueryParameters.Any())
			{
				var queryObject = QueryParametersParser.GetQueryObject(generatedMethod, method, objectName);
				generatedMethod.Query = queryObject;
				if (!queryObjects.ContainsKey(queryObject.Name))
					queryObjects.Add(queryObject.Name, queryObject);
			}
		}

		private void GetHeaders(string objectName, Method method, ClientGeneratorMethod generatedMethod, IDictionary<string, ApiObject> headerObjects)
		{
			if (method.Headers != null && method.Headers.Any())
			{
				var headerObject = HeadersParser.GetHeadersObject(generatedMethod, method, objectName);
				generatedMethod.Header = headerObject;
				headerObjects.Add(headerObject.Name, headerObject);
			}
		}

		private void GetResponseHeaders(string objectName, ClientGeneratorMethod generatedMethod, Method method, IDictionary<string, ApiObject> responseHeadersObjects)
		{
			generatedMethod.ResponseHeaders = new Dictionary<HttpStatusCode, ApiObject>();
			foreach (var resp in method.Responses.Where(r => r.Headers != null && r.Headers.Any()))
			{
				var headerObject = HeadersParser.GetHeadersObject(generatedMethod, resp, objectName);
				generatedMethod.ResponseHeaders.Add(ParserHelpers.GetHttpStatusCode(resp.Code), headerObject);
				responseHeadersObjects.Add(headerObject.Name, headerObject);
			}

			if (!generatedMethod.ResponseHeaders.Any())
			{
				generatedMethod.ResponseHeaderType = DefaultHeaderType;
			}
			else if (generatedMethod.ResponseHeaders.Count == 1)
			{
				generatedMethod.ResponseHeaderType = ClientGeneratorMethod.ModelsNamespacePrefix + generatedMethod.ResponseHeaders.First().Value.Name;
			}
			else
			{
				CreateMultipleType(generatedMethod, responseHeadersObjects);
			}
		}

		private void CreateMultipleType(ClientGeneratorMethod generatedMethod, IDictionary<string, ApiObject> responseHeadersObjects)
		{
			var properties = BuildProperties(generatedMethod);

			var name = NetNamingMapper.GetObjectName("Multiple" + generatedMethod.Name + "Header");

			var apiObject = new ApiObject
			{
				Name = name,
				Description = "Multiple Header Types " + string.Join(", ", properties.Select(p => p.Name)),
				Properties = properties,
				IsMultiple = true
			};
			responseHeadersObjects.Add(new KeyValuePair<string, ApiObject>(name, apiObject));

			generatedMethod.ResponseHeaderType = ClientGeneratorMethod.ModelsNamespacePrefix + name;
		}

		private static List<Property> BuildProperties(ClientGeneratorMethod generatedMethod)
		{
			var properties = generatedMethod.ResponseHeaders
				.Select(kv => new Property
				              {
					              Name = kv.Value.Name,
					              Description = kv.Value.Description,
					              Example = kv.Value.Example,
					              StatusCode = kv.Key,
					              Type = kv.Value.Name
				              })
				.ToList();
			return properties;
		}
	}
}