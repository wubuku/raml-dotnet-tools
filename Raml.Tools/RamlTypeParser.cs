using System.Collections.Generic;
using System.Linq;
using Raml.Common;
using Raml.Parser.Expressions;

namespace Raml.Tools
{
    public class RamlTypeParser
    {

        public static void Parse(ICollection<RamlType> ramlTypes, IDictionary<string, ApiObject> schemaObjects)
        {
            foreach (var ramlType in ramlTypes)
            {
                if (ramlType.Properties.Count == 1 && ramlType.Properties.First().Key.StartsWith("[") &&
                    ramlType.Properties.First().Key.EndsWith("]"))
                {
                    // Map
                    if (ramlType.Properties.First().Value.Type == "object")
                    {

                    }
                    else
                    {
                        schemaObjects.Add(ramlType.Name, new ApiObject
                        {
                            Type = "IDictionary<string," + ramlType.Properties.First().Value.Type + ">",
                            Name = ramlType.Name,
                            BaseClass = string.Empty,
                            Description = ramlType.Description,
                            Example = GetExampe(ramlType.Example, ramlType.Examples),
                            Properties = new Property[0]
                        });
                    }
                }
                else
                {
                    schemaObjects.Add(ramlType.Name, GetApiObjectFromRamlType(ramlType));
                }
            }
        }

        private static ApiObject GetApiObjectFromRamlType(RamlType ramlType, string defaultName = "")
        {
            var name = string.IsNullOrWhiteSpace(ramlType.Name) ? defaultName : ramlType.Name;
            return new ApiObject
            {
                Type = name,
                Name = name,
                BaseClass = ramlType.Type != "object" ? ramlType.Type : string.Empty,
                Description = ramlType.Description,
                Example = GetExampe(ramlType.Example, ramlType.Examples),
                Properties = GetProperties(ramlType.Properties)
            };
        }

        private static IList<Property> GetProperties(IDictionary<string, Parameter> properties)
        {
            var props = new List<Property>();
            foreach (var kv in properties)
            {
                var prop = kv.Value;
                props.Add(new Property
                {
                    Minimum = ToDouble(prop.Minimum),
                    Maximum = ToDouble(prop.Maximum),
                    Type = prop.Type == "object" ? NetNamingMapper.GetObjectName(kv.Key) : NetTypeMapper.Map(prop.Type),
                    MaxLength = prop.MaxLength,
                    MinLength = prop.MinLength,
                    Name = NetNamingMapper.GetObjectName(kv.Key),
                    Required = prop.Required,
                    Example = prop.Example,
                    Description = prop.Description,
                    IsEnum = prop.Enum != null && prop.Enum.Any(),
                    OriginalName = kv.Key
                });

                if (prop.Type == "object")
                {
                    GetApiObjectFromRamlType(prop, NetNamingMapper.GetObjectName(kv.Key));
                }
            }
            return props;
        }

        private static double? ToDouble(decimal? value)
        {
            if (value == null)
                return null;

            return (double)value.Value;
        }

        private static string GetExampe(string example, IEnumerable<string> examples)
        {
            var result = string.IsNullOrWhiteSpace(example) ? example + "\r\n" : string.Empty;
            if (examples != null)
                result += string.Join("\r\n", examples);

            return result;
        } 
    }
}