using System;
using System.Collections.Generic;
using System.Linq;
using Raml.Common;
using Raml.Parser.Expressions;
using Raml.Tools.JSON;
using Raml.Tools.XML;

namespace Raml.Tools
{
    public class RamlTypeParser
    {
        private readonly IDictionary<string, ApiObject> schemaObjects;
        private readonly string targetNamespace;

        private readonly IDictionary<string, string> warnings;
        private readonly IDictionary<string, ApiEnum> enums;

        public RamlTypeParser(IDictionary<string, ApiObject> schemaObjects, string targetNamespace, IDictionary<string, ApiEnum> enums, IDictionary<string, string> warnings)
        {
            this.schemaObjects = schemaObjects;
            this.targetNamespace = targetNamespace;
            this.enums = enums;
            this.warnings = warnings;
        }

        public void Parse(IDictionary<string, RamlType> ramlTypes)
        {
            foreach (var ramlType in ramlTypes)
            {
                var apiObject = ParseRamlType(ramlType);
                if(apiObject != null)
                    schemaObjects.Add(ramlType.Key, apiObject);
            }
        }

        public ApiObject ParseInline(string key, RamlType inline, IDictionary<string, ApiObject> objects)
        {
            return ParseRamlType(new KeyValuePair<string, RamlType>(key, inline));
        }

        private ApiObject ParseRamlType(KeyValuePair<string, RamlType> ramlType)
        {
            if (ramlType.Value.External != null)
            {
                return ParseExternal(ramlType);
            }
            if (ramlType.Value.Scalar != null)
            {
                return ParseScalar(ramlType);
            }
            if (ramlType.Value.Object != null)
            {
                return ParseObject(ramlType);
            }
            if (ramlType.Value.Array != null)
            {
                return ParseArray(ramlType);
            }
            throw new InvalidOperationException("Cannot parse type of " + ramlType.Key);
        }

        private ApiObject ParseNestedType(RamlType ramlType, string keyOrTypeName)
        {
            var pair = new KeyValuePair<string, RamlType>(keyOrTypeName, ramlType);
            return ParseRamlType(pair);
        }


        private ApiObject ParseArray(KeyValuePair<string, RamlType> ramlType)
        {
            var typeOfArray = GetTypeOfArray(ramlType);

            var baseType = CollectionTypeHelper.GetBaseType(typeOfArray);
            if (!NetTypeMapper.IsPrimitiveType(baseType) &&
                ramlType.Value.Array.Items != null && ramlType.Value.Array.Items.Type == "object")
            {
                if (baseType == typeOfArray)
                    baseType = typeOfArray + "Item";

                var itemType = ParseNestedType(ramlType.Value.Array.Items, baseType);
                schemaObjects.Add(baseType, itemType);
                typeOfArray = CollectionTypeHelper.GetCollectionType(baseType);
            }

            return new ApiObject
            {
                IsArray = true,
                Name = NetNamingMapper.GetObjectName(ramlType.Key),
                Description = ramlType.Value.Description,
                Example = ramlType.Value.Example,
                Type = typeOfArray
            };
        }

        private static string GetTypeOfArray(KeyValuePair<string, RamlType> ramlType)
        {
            if (!string.IsNullOrWhiteSpace(ramlType.Value.Type))
            {
                var pureType = ramlType.Value.Type.EndsWith("[]") ? ramlType.Value.Type.Substring(0, ramlType.Value.Type.Length - 2) : ramlType.Value.Type;

                if (pureType != "array" && pureType != "object")
                    return CollectionTypeHelper.GetCollectionType(pureType);
                
            }
            if (!string.IsNullOrWhiteSpace(ramlType.Value.Array.Items.Type))
            {
                if (ramlType.Value.Array.Items.Type != "object")
                    return CollectionTypeHelper.GetCollectionType(ramlType.Value.Array.Items.Type);
            }

            if(!string.IsNullOrWhiteSpace(ramlType.Value.Array.Items.Name))
                return CollectionTypeHelper.GetCollectionType(ramlType.Value.Array.Items.Name);

            return ramlType.Key;
        }

        private ApiObject ParseScalar(KeyValuePair<string, RamlType> ramlType)
        {
            // TODO: check, should not really be an object, but is needed to map to primitive type...
            if (ramlType.Value.Scalar.Enum != null && ramlType.Value.Scalar.Enum.Any())
            {
                if (enums.ContainsKey(ramlType.Key))
                    return null;

                enums.Add(ramlType.Key, new ApiEnum
                {
                    Name = NetNamingMapper.GetObjectName(ramlType.Key),
                    Description = ramlType.Value.Description,
                    Values = ramlType.Value.Scalar.Enum.ToList()
                });
                return null;
            }
            
            return new ApiObject
            {
                Type = NetTypeMapper.Map(ramlType.Value.Scalar.Type),
                Name = NetNamingMapper.GetObjectName(ramlType.Key),
                Example = ramlType.Value.Example,
                Description = ramlType.Value.Description,
                IsScalar = true,
            };
        }

        private ApiObject ParseExternal(KeyValuePair<string,RamlType> ramlType)
        {
            if (!string.IsNullOrWhiteSpace(ramlType.Value.External.Schema))
            {
                return
                    new JsonSchemaParser().Parse(ramlType.Key, ramlType.Value.External.Schema, schemaObjects, warnings,
                        enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());
            }

            if (!string.IsNullOrWhiteSpace(ramlType.Value.External.Xml))
            {
                return new XmlSchemaParser().Parse(ramlType.Key, ramlType.Value.External.Xml, schemaObjects, targetNamespace);
            }

            throw new InvalidOperationException("Cannot parse external type of " + ramlType.Key);
        }

        private ApiObject ParseObject(KeyValuePair<string, RamlType> ramlType)
        {
            if (ramlType.Value.Object.Properties.Count == 1 &&
                ramlType.Value.Object.Properties.First().Key.StartsWith("[") &&
                ramlType.Value.Object.Properties.First().Key.EndsWith("]"))
            {
                return ParseMap(ramlType.Value, ramlType.Key);
            }
            else
            {

                return GetApiObjectFromObject(ramlType.Value, ramlType.Key);
            }
        }

        private ApiObject ParseMap(RamlType ramlType, string key)
        {
            var name = NetNamingMapper.GetObjectName(key ?? ramlType.Name);
            var type = ramlType.Object.Properties.First().Value.Type;

            if (ramlType.Object.Properties.First().Value.Object != null && ramlType.Object.Properties.First().Value.Type == "object")
            {
                var itemName = name + "Item";
                var pair = new KeyValuePair<string, RamlType>(itemName, ramlType.Object.Properties.First().Value);
                var nestedObject = ParseObject(pair);
                type = nestedObject.Name;
                schemaObjects.Add(itemName, nestedObject);
            }

            type = RamlTypesHelper.DecodeRaml1Type(type);

            return new ApiObject
            {
                Type = "IDictionary<string," + type + ">",
                Name = name,
                BaseClass = string.Empty,
                Description = ramlType.Description,
                Example = GetExampe(ramlType.Example, ramlType.Examples),
                Properties = new Property[0]
            };
        }

        private ApiObject GetApiObjectFromObject(RamlType ramlType, string defaultName = "")
        {
            var name = defaultName == "" ? ramlType.Name : defaultName;

            if (string.IsNullOrWhiteSpace(name))
                name = "Type" + DateTime.Now.Ticks;

            return new ApiObject
            {
                Type = NetNamingMapper.GetObjectName(name),
                Name = NetNamingMapper.GetObjectName(name),
                BaseClass = ramlType.Type != "object" ? ramlType.Type : string.Empty,
                Description = ramlType.Description,
                Example = GetExampe(ramlType.Example, ramlType.Examples),
                Properties = GetProperties(ramlType.Object.Properties)
            };
        }

        private IList<Property> GetProperties(IDictionary<string, RamlType> properties)
        {
            var props = new List<Property>();
            foreach (var kv in properties)
            {
                var prop = kv.Value;
                if (prop.Scalar != null)
                {
                    var newProp = GetPropertyFromScalar(prop, kv);
                    props.Add(newProp);
                    continue;
                }
                if (prop.Object != null)
                {
                    var name = NetNamingMapper.GetPropertyName(kv.Key);
                    if (!schemaObjects.ContainsKey(name))
                    {
                        var newApiObject = GetApiObjectFromObject(prop, name);
                        schemaObjects.Add(name, newApiObject);
                    }
                    props.Add(new Property { Name = name, Type = name, Required = prop.Required, OriginalName = kv.Key.TrimEnd('?')});
                    continue;
                }
                if (prop.Array != null)
                {
                    var name = NetNamingMapper.GetPropertyName(kv.Key);
                    var type = kv.Value.Type;
                    if (kv.Value.Array.Items != null)
                    {
                        var obj = ParseArray(kv);
                        type = CollectionTypeHelper.GetCollectionType(obj.Type);
                    }
                    if (type.EndsWith("[]"))
                    {
                        type = type.Substring(0, type.Length - 2);
                        type = CollectionTypeHelper.GetCollectionType(type);
                    }

                    props.Add(new Property { Name = name, Type = type, Required = prop.Required, OriginalName = kv.Key.TrimEnd('?') });

                    continue;
                }
            }
            return props;
        }

        private Property GetPropertyFromScalar(RamlType prop, KeyValuePair<string, RamlType> kv)
        {
            if (prop.Scalar.Enum != null && prop.Scalar.Enum.Any())
            {
                if (!enums.ContainsKey(kv.Key))
                {
                    var apiEnum = new ApiEnum
                    {
                        Name = NetNamingMapper.GetPropertyName(kv.Key),
                        Description = kv.Value.Description,
                        Values = kv.Value.Scalar.Enum.ToList()
                    };
                    enums.Add(kv.Key, apiEnum);
                }
            }

            return new Property
            {
                Minimum = ToDouble(prop.Scalar.Minimum),
                Maximum = ToDouble(prop.Scalar.Maximum),
                Type = GetPropertyType(prop, kv),
                MaxLength = prop.Scalar.MaxLength,
                MinLength = prop.Scalar.MinLength,
                Name = NetNamingMapper.GetPropertyName(kv.Key),
                Required = prop.Required,
                Example = prop.Example,
                Description = prop.Description,
                IsEnum = prop.Scalar.Enum != null && prop.Scalar.Enum.Any(),
                OriginalName = kv.Key.TrimEnd('?')
            };
        }

        private static string GetPropertyType(RamlType prop, KeyValuePair<string, RamlType> kv)
        {
            if (string.IsNullOrWhiteSpace(prop.Type))
                return "string";

            return prop.Type == "object" || (prop.Scalar.Enum != null && prop.Scalar.Enum.Any()) 
                ? NetNamingMapper.GetPropertyName(kv.Key) 
                : NetTypeMapper.Map(prop.Scalar.Type);
        }

        private double? ToDouble(decimal? value)
        {
            if (value == null)
                return null;

            return (double)value.Value;
        }

        private string GetExampe(string example, IEnumerable<string> examples)
        {
            var result = string.IsNullOrWhiteSpace(example) ? example + "\r\n" : string.Empty;
            if (examples != null)
                result += string.Join("\r\n", examples);

            return result;
        } 
    }
}