using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools
{
	public class UriParametersGenerator
	{
		public void Generate(Resource resource, string url, ClientGeneratorMethod clientGeneratorMethod,
			IDictionary<string, ApiObject> uriParameterObjects)
		{
			var parameters = GetUriParameters(resource, url).ToArray();
			clientGeneratorMethod.UriParameters = parameters;

			if (!parameters.Any())
				return;

			var name = NetNamingMapper.GetObjectName(url) + "UriParameters";
			clientGeneratorMethod.UriParametersType = name;
			if (uriParameterObjects.ContainsKey(name))
				return;

			var properties = new List<Property>();
			if (resource.BaseUriParameters != null)
				properties.AddRange(QueryParametersParser.ConvertParametersToProperties(resource.BaseUriParameters));

			if (resource.UriParameters != null)
				properties.AddRange(QueryParametersParser.ConvertParametersToProperties(resource.UriParameters)
					.Where(up => properties.All(p => !String.Equals(up.Name, p.Name, StringComparison.InvariantCultureIgnoreCase))));

			var urlParameters = ExtractParametersFromUrl(url).ToArray();

			foreach (var urlParameter in urlParameters)
			{
				var property = ConvertGeneratorParamToProperty(urlParameter);
				if (properties.All(p => !String.Equals(property.Name, p.Name, StringComparison.InvariantCultureIgnoreCase)))
					properties.Add(property);
			}

			var apiObject = new ApiObject
			                {
				                Name = name,
				                Description = "Uri Parameters for resource " + resource.RelativeUri,
				                Properties = properties
			                };
			uriParameterObjects.Add(name, apiObject);
		}

		public IEnumerable<GeneratorParameter> GetUriParameters(Resource resource, string url)
		{
			var parameters = resource.BaseUriParameters
					.Select(p => new GeneratorParameter { Name = p.Key, Type = NetTypeMapper.Map(p.Value.Type), Description = p.Value.Description })
					.ToList();
			parameters.AddRange(resource.UriParameters
				.Select(p => new GeneratorParameter { Name = p.Key, Type = NetTypeMapper.Map(p.Value.Type), Description = p.Value.Description })
				.ToList());

			var urlParameters = ExtractParametersFromUrl(url).ToArray();
			parameters.AddRange(urlParameters.Where(up => parameters.All(p => up.Name != p.Name)).ToArray());
			return parameters;
		}

		protected IEnumerable<GeneratorParameter> ExtractParametersFromUrl(string url)
		{
			var parameters = new List<GeneratorParameter>();
			if (string.IsNullOrWhiteSpace(url) || !url.Contains("{"))
				return parameters;

			var regex = new Regex("{([^}]+)}");
			var matches = regex.Matches(url);
			parameters.AddRange(matches.Cast<Match>().Select(match => new GeneratorParameter { Name = match.Groups[1].Value, Type = "string" }));
			return parameters;
		}

		private static Property ConvertGeneratorParamToProperty(GeneratorParameter p)
		{
			return new Property
			       {
				       Name = NetNamingMapper.Capitalize(p.Name),
				       Description = p.Description,
				       Type = p.Type,
				       Required = true
			       };
		}
	}
}