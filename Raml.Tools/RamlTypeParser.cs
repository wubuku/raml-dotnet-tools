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

        private readonly IDictionary<string, string> warnings = new Dictionary<string, string>();
        private readonly IDictionary<string, ApiEnum> enums = new Dictionary<string, ApiEnum>();

        public RamlTypeParser(IDictionary<string, ApiObject> schemaObjects, string targetNamespace)
        {
            this.schemaObjects = schemaObjects;
            this.targetNamespace = targetNamespace;
        }

        public void Parse(IDictionary<string, RamlType> ramlTypes)
        {
            foreach (var ramlType in ramlTypes)
            {
                schemaObjects.Add(ramlType.Key, ParseRamlType(ramlType));
            }
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
            return GetApiObjectFromArray(ramlType);
        }

        private ApiObject GetApiObjectFromArray(KeyValuePair<string, RamlType> ramlType)
        {
            var typeOfArray = GetTypeOfArray(ramlType);

            if (!NetTypeMapper.IsPrimitiveType(CollectionTypeHelper.GetBaseType(typeOfArray)) &&
                ramlType.Value.Array.Items != null)
            {
                var itemType = ParseNestedType(ramlType.Value.Array.Items, CollectionTypeHelper.GetBaseType(typeOfArray));
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
                if (pureType != "object")
                    return CollectionTypeHelper.GetCollectionType(pureType);
                
            }
            if (!string.IsNullOrWhiteSpace(ramlType.Value.Array.Items.Type))
            {
                if (ramlType.Value.Array.Items.Type != "object")
                    return CollectionTypeHelper.GetCollectionType(ramlType.Value.Array.Items.Type);
            }

            if(!string.IsNullOrWhiteSpace(ramlType.Value.Array.Items.Name))
                return CollectionTypeHelper.GetCollectionType(ramlType.Value.Array.Items.Name);

            throw new InvalidOperationException("Cannot determine array type of " + ramlType.Key);
        }

        private ApiObject ParseScalar(KeyValuePair<string, RamlType> ramlType)
        {
            // TODO: check, should not really be an object, but is needed to map to primitive type...
            return new ApiObject
            {
                Type = ramlType.Value.Scalar.Type,
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
                Type = name,
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
                    var name = NetNamingMapper.GetObjectName(kv.Key);
                    var newApiObject =  GetApiObjectFromObject(prop, name);
                    schemaObjects.Add(name, newApiObject);
                    props.Add(new Property { Name = name, Type = name, Required = prop.Required });
                    continue;
                }
            }
            return props;
        }

        private Property GetPropertyFromScalar(RamlType prop, KeyValuePair<string, RamlType> kv)
        {
            return new Property
            {
                Minimum = ToDouble(prop.Scalar.Minimum),
                Maximum = ToDouble(prop.Scalar.Maximum),
                Type = prop.Type == "object" ? NetNamingMapper.GetObjectName(kv.Key) : NetTypeMapper.Map(prop.Scalar.Type),
                MaxLength = prop.Scalar.MaxLength,
                MinLength = prop.Scalar.MinLength,
                Name = NetNamingMapper.GetObjectName(kv.Key),
                Required = prop.Required,
                Example = prop.Example,
                Description = prop.Description,
                IsEnum = prop.Scalar.Enum != null && prop.Scalar.Enum.Any(),
                OriginalName = kv.Key
            };
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