using System.Collections.Generic;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools
{
	public class QueryParametersParser
	{
		public static ApiObject GetQueryObject(ClientGeneratorMethod generatedMethod, Method method, string objectName)
		{
			var queryObject = new ApiObject { Name = generatedMethod.Name + objectName + "Query" };
			queryObject.Properties = ParseParameters(method);


			return queryObject;
		}

		public static IList<Property> ParseParameters(Method method)
		{
			return ConvertParametersToProperties(method.QueryParameters);
		}

		public static IList<Property> ConvertParametersToProperties(IEnumerable<KeyValuePair<string, Parameter>> parameters)
		{
			var properties = new List<Property>();
			foreach (var parameter in parameters)
			{
                var description = ParserHelpers.RemoveNewLines(parameter.Value.Description);

				properties.Add(new Property
				               {
					               Type =
						               NetTypeMapper.Map(parameter.Value.Type) +
						               (NetTypeMapper.Map(parameter.Value.Type) == "string" || parameter.Value.Required ? "" : "?"),
					               Name = NetNamingMapper.GetPropertyName(parameter.Key),
                                   OriginalName = parameter.Key,
					               Description = description,
					               Example = parameter.Value.Example,
					               Required = parameter.Value.Required
				               });
			}
			return properties;
		}
	}
}