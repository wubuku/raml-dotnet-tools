using System.Collections.Generic;
using Raml.Common;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools
{
    public class HeadersParser
    {
        public static ApiObject GetHeadersObject(ClientGeneratorMethod generatedMethod, Method method, string objectName)
        {
            return new ApiObject
            {
                Name = generatedMethod.Name + objectName + "Header",
                Properties = ParseHeaders(method)
            };
        }

        public static ApiObject GetHeadersObject(ClientGeneratorMethod generatedMethod, Response response, string objectName)
        {
           return new ApiObject
            {
                Name = generatedMethod.Name + objectName + ParserHelpers.GetStatusCode(response.Code) + "ResponseHeader",
                Properties = ParseHeaders(response)
            };
        }

        public static IList<Property> ParseHeaders(Method method)
        {
            return ConvertHeadersToProperties(method.Headers);
        }

        public static IList<Property> ParseHeaders(Response response)
        {
            return ConvertHeadersToProperties(response.Headers);
        }

        public static IList<Property> ConvertHeadersToProperties(IEnumerable<Parameter> headers)
        {
            var properties = new List<Property>();
            if (headers == null)
            {
                return properties;
            }

            foreach (var header in headers)
            {
                var description = ParserHelpers.RemoveNewLines(header.Description);

                var type = NetTypeMapper.Map(header.Type);
                var typeSuffix = (type == "string" || header.Required ? "" : "?");

                properties.Add(new Property
                               {
                                   Type = type + typeSuffix,
                                   Name = NetNamingMapper.GetPropertyName(header.DisplayName),
                                   OriginalName = header.DisplayName,
                                   Description = description,
                                   Example = header.Example,
                                   Required = header.Required
                               });
            }
            return properties;
        }
    }
}