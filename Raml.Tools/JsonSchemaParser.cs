using Newtonsoft.Json.Schema;
using Raml.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Raml.Tools
{
    public class JsonSchemaParser
	{
	    
	    private readonly string[] suffixes = { "A", "B", "C", "D", "E", "F", "G" };
        private readonly ICollection<string> ids  = new Collection<string>();
        private IDictionary<string, ApiObject> otherObjects = new Dictionary<string, ApiObject>();
        public ApiObject Parse(string key, string jsonSchema, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings, IDictionary<string, ApiEnum> enums, IDictionary<string, ApiObject> otherObjects)
        {
            this.otherObjects = otherObjects;
			var obj = new ApiObject
			          {
				          Name = NetNamingMapper.GetObjectName(key),
				          Properties = new List<Property>(),
                          JSONSchema = jsonSchema.Replace(Environment.NewLine, "").Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"")
			          };
			JsonSchema schema = null;
			Newtonsoft.JsonV4.Schema.JsonSchema v4Schema = null;
		    if (jsonSchema.Contains("\"oneOf\":"))
		    {
		        v4Schema = ParseV4Schema(key, jsonSchema, warnings, objects);
		    }
		    else
		    {
		        schema = ParseV3OrV4Schema(key, jsonSchema, warnings, ref v4Schema, objects);
		    }

		    if (schema == null && v4Schema == null)
				return obj;

			if (schema != null)
			{
				if (schema.Type == JsonSchemaType.Array)
				{
					obj.IsArray = true;
					if (schema.Items != null && schema.Items.Any())
						ParseProperties(objects, obj.Properties, schema.Items.First().Properties, enums);
				}
				else
				{
					ParseProperties(objects, obj.Properties, schema.Properties, enums);
				}
			}
			else
			{
				if (v4Schema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Array)
				{
					obj.IsArray = true;
					if (v4Schema.Items != null && v4Schema.Items.Any())
						ParseProperties(objects, obj.Properties, v4Schema.Items.First(), enums);
				}
				else
				{
					ParseProperties(objects, obj.Properties, v4Schema, enums);
				}
			}
			return obj;
		}

        private static JsonSchema ParseV3OrV4Schema(string key, string jsonSchema, IDictionary<string, string> warnings, 
            ref Newtonsoft.JsonV4.Schema.JsonSchema v4Schema, IDictionary<string, ApiObject> objects)
        {
            JsonSchema schema = null;
            try
            {
                schema = JsonSchema.Parse(jsonSchema, new JsonSchemaCustomResolver(objects));
            }
            catch (Exception exv3) // NewtonJson does not support Json Schema v4
            {
                try
                {
                    schema = null;
                    v4Schema = Newtonsoft.JsonV4.Schema.JsonSchema.Parse(jsonSchema, new JsonSchemaCustomV4Resolver(objects));
                }
                catch (Exception exv4)
                {
                    if (!warnings.ContainsKey(key))
                        warnings.Add(key,
                            "Could not parse JSON Schema. v3 parser message: " +
                            exv3.Message.Replace("\r\n", string.Empty).Replace("\n", string.Empty) +
                            ". v4 parser message: " +
                            exv4.Message.Replace("\r\n", string.Empty).Replace("\n", string.Empty));
                }
            }
            return schema;
        }

        private static Newtonsoft.JsonV4.Schema.JsonSchema ParseV4Schema(string key, string jsonSchema, IDictionary<string, string> warnings, IDictionary<string, ApiObject> objects)
        {
            Newtonsoft.JsonV4.Schema.JsonSchema v4Schema = null;
            try
            {
                v4Schema = Newtonsoft.JsonV4.Schema.JsonSchema.Parse(jsonSchema, new JsonSchemaCustomV4Resolver(objects));
            }
            catch (Exception exv4)
            {
                if (!warnings.ContainsKey(key))
                    warnings.Add(key,
                        "Could not parse JSON Schema. " +
                        exv4.Message.Replace("\r\n", string.Empty).Replace("\n", string.Empty));
            }
            return v4Schema;
        }

        private string ParseObject(string key, IDictionary<string, JsonSchema> schema, IDictionary<string, ApiObject> objects, IDictionary<string, ApiEnum> enums)
		{
			if (schema == null)
				return null;

			var obj = new ApiObject
			{
				Name = NetNamingMapper.GetObjectName(key),
				Properties = ParseSchema(schema, objects, enums)
			};

			// Avoid duplicated keys and names or no properties
            if (objects.ContainsKey(key) || objects.Any(o => o.Value.Name == obj.Name) || !obj.Properties.Any() || otherObjects.ContainsKey(key) || otherObjects.Any(o => o.Value.Name == obj.Name))
            {
                if (HasSameProperties(obj, objects, key)) 
                    return key;

                obj.Name = GetUniqueName(objects, obj.Name);
                key = GetUniqueKey(objects, key);
            }

			objects.Add(key, obj);
            return key;
		}

        private bool HasSameProperties(ApiObject apiObject, IDictionary<string, ApiObject> objects, string key)
        {
            var obj = objects.ContainsKey(key) ? objects[key] : objects.FirstOrDefault(o => o.Value.Name == apiObject.Name).Value;
            if(obj == null)
                obj = otherObjects.ContainsKey(key) ? otherObjects[key] : otherObjects.First(o => o.Value.Name == apiObject.Name).Value;
            
            if(obj.Properties.Count != apiObject.Properties.Count)
                return false;

            return apiObject.Properties.All(property => obj.Properties.Any(p => p.Name == property.Name && p.Type == property.Type));
        }


        private IList<Property> ParseSchema(IDictionary<string, JsonSchema> schema, IDictionary<string, ApiObject> objects, IDictionary<string, ApiEnum> enums)
		{
			var props = new List<Property>();
			foreach (var kv in schema)
			{
                var isEnum = kv.Value.Enum != null && kv.Value.Enum.Any();

			    var enumName = string.Empty;
                if (isEnum)
                {
                    enumName = ParseEnum(kv.Key, kv.Value, enums, kv.Value.Description);
                }

				var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(kv.Key),
                               OriginalName = kv.Key,
                               Type = GetType(kv, isEnum, enumName),
					           Description = kv.Value.Description,
                               IsEnum = isEnum
				           };

				ParseComplexTypes(objects, kv.Value, prop, kv, kv.Key, enums);
				props.Add(prop);
			}
			return props;
		}


		private void ParseProperties(IDictionary<string, ApiObject> objects, IList<Property> props, Newtonsoft.JsonV4.Schema.JsonSchema schema, IDictionary<string, ApiEnum> enums)
		{
            var properties = schema.Properties;

			foreach (var property in properties)
			{
                if ((property.Value.Enum != null && !property.Value.Enum.Any()) && (property.Value.Type == null || property.Value.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Null ||
				    property.Value.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.None))
					continue;

                var isEnum = property.Value.Enum != null && property.Value.Enum.Any();

			    var enumName = string.Empty;
			    if (isEnum)
                {
                    enumName = ParseEnum(property.Key, property.Value, enums, property.Value.Description);
                }

			    var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(property.Key),
					           Type = GetType(property, isEnum, enumName),
                               OriginalName = property.Key,
					           Description = property.Value.Description,
                               IsEnum = isEnum
				           };



				ParseComplexTypes(objects, schema, property.Value, prop, property, enums);
				props.Add(prop);
			}
		}

        private static string GetType(KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property, bool isEnum, string enumName)
        {
            if (property.Value.OneOf != null && property.Value.OneOf.Count > 0)
                return NetNamingMapper.GetObjectName(property.Key);

            if (isEnum) 
                return enumName;

            if (!string.IsNullOrWhiteSpace(NetTypeMapper.Map(property.Value.Type)))
                return NetTypeMapper.Map(property.Value.Type);

            if (HasMultipleTypes(property))
                return HandleMultipleTypes(property);

            if (!string.IsNullOrWhiteSpace(property.Value.Id))
                return NetNamingMapper.GetObjectName(property.Value.Id);

            return NetNamingMapper.GetObjectName(property.Key);
        }

        private static string HandleMultipleTypes(KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property)
        {
            var type = "object";
            var types = property.Value.Type.ToString().Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            if (types.Length == 2)
            {
                type = types[0] == "Null"
                    ? NetTypeMapper.Map(types[1].ToLowerInvariant())
                    : NetTypeMapper.Map(types[0].ToLowerInvariant());
                type = IsNullableType(type) ? type + "?" : type;
            }
            return type;
        }

        private static bool IsNullableType(string type)
        {
            return type != "string";
        }

        private static bool HasMultipleTypes(KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property)
        {
            return property.Value.Type != null && property.Value.Type.ToString().Contains(",") && property.Value.Type.ToString().Contains("Null") && !property.Value.Type.ToString().Contains("Object");
        }

        private static string GetType(KeyValuePair<string, JsonSchema> property, bool isEnum, string enumName)
        {
            if (isEnum)
                return enumName;

            if (!string.IsNullOrWhiteSpace(NetTypeMapper.Map(property.Value.Type)))
                return NetTypeMapper.Map(property.Value.Type);

            if (HasMultipleTypes(property))
                return HandleMultipleTypes(property);

            if (!string.IsNullOrWhiteSpace(property.Value.Id))
                return NetNamingMapper.GetObjectName(property.Value.Id);

            return NetNamingMapper.GetObjectName(property.Key);
        }

        private static string HandleMultipleTypes(KeyValuePair<string, JsonSchema> property)
        {
            var type = "object";
            var types = property.Value.Type.ToString().Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);
            if (types.Length == 2)
            {
                type = types[0] == "Null"
                    ? NetTypeMapper.Map(types[1].ToLowerInvariant())
                    : NetTypeMapper.Map(types[0].ToLowerInvariant());
                type = IsNullableType(type) ? type + "?" : type;
            }
            return type;
        }

        private static bool HasMultipleTypes(KeyValuePair<string, JsonSchema> property)
        {
            return property.Value.Type != null && property.Value.Type.ToString().Contains(",") && property.Value.Type.ToString().Contains("Null") && !property.Value.Type.ToString().Contains("Object");
        }

        private void ParseComplexTypes(IDictionary<string, ApiObject> objects, Newtonsoft.JsonV4.Schema.JsonSchema schema, Newtonsoft.JsonV4.Schema.JsonSchema propertySchema, Property prop, KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property, IDictionary<string, ApiEnum> enums)
		{
            if (propertySchema.Type.HasValue
                && (propertySchema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Object || propertySchema.Type.Value.ToString().Contains("Object")) 
                && propertySchema.Properties != null)
			{
                if (!string.IsNullOrWhiteSpace(schema.Id) && ids.Contains(schema.Id))
                    return;

                if (!string.IsNullOrWhiteSpace(schema.Id))
                    ids.Add(schema.Id);

			    var type = string.IsNullOrWhiteSpace(property.Value.Id) ? property.Key : property.Value.Id;
			    ParseObject(type, propertySchema.Properties, objects, enums);
				prop.Type = NetNamingMapper.GetObjectName(type);
			}
            else if (propertySchema.Type.HasValue
                && propertySchema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Object && propertySchema.OneOf != null && propertySchema.OneOf.Count > 0 && schema.Definitions != null && schema.Definitions.Count > 0)
            {
                string baseTypeName = NetNamingMapper.GetObjectName(property.Key);

                if (objects.ContainsKey(baseTypeName))
                    return;

                objects.Add(baseTypeName,
                    new ApiObject
                    {
                        Name = baseTypeName,
                        Properties = new List<Property>()
                    });

                foreach(var innerSchema in propertySchema.OneOf)
                {
                    var definition = schema.Definitions.FirstOrDefault(k => k.Value == innerSchema);
                    ParseObject(property.Key + definition.Key, innerSchema.Properties, objects, enums, baseTypeName);
                                       
                }

                prop.Type = baseTypeName;
            }
            else if (propertySchema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Array)
            {
                ParseArray(objects, propertySchema, prop, property, enums);
            }
            
		}

		private void ParseObject(string key, IDictionary<string, Newtonsoft.JsonV4.Schema.JsonSchema> schema, IDictionary<string, ApiObject> objects, IDictionary<string, ApiEnum> enums, string baseClass = null)
		{
			var obj = new ApiObject
			          {
				          Name = NetNamingMapper.GetObjectName(key),
				          Properties = ParseSchema(schema, objects, enums),
                          BaseClass = baseClass
			          };

			// Avoid duplicated keys and names
			if (objects.ContainsKey(key) || objects.Any(o => o.Value.Name == obj.Name) || !obj.Properties.Any())
				return;

			objects.Add(key, obj);
		}

		private IList<Property> ParseSchema(IDictionary<string, Newtonsoft.JsonV4.Schema.JsonSchema> schema, IDictionary<string, ApiObject> objects, IDictionary<string, ApiEnum> enums)
		{
			var props = new List<Property>();
			foreach (var kv in schema)
			{
                var isEnum = kv.Value.Enum != null && kv.Value.Enum.Any();

                var enumName = string.Empty;
                if (isEnum)
                {
                    enumName = ParseEnum(kv.Key, kv.Value, enums, kv.Value.Description);
                }

			    var prop = new Property
                {
                    Name = NetNamingMapper.GetPropertyName(kv.Key),
                    OriginalName = kv.Key,
                    Type = GetType(kv, isEnum, enumName),
                    Description = kv.Value.Description,
                    IsEnum = isEnum
                };


				ParseComplexTypes(objects, null, kv.Value, prop, kv, enums);
				props.Add(prop);
			}
			return props;
		}

		private void ParseArray(IDictionary<string, ApiObject> objects, Newtonsoft.JsonV4.Schema.JsonSchema schema, Property prop, KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property, IDictionary<string, ApiEnum> enums)
		{
			var netType = NetTypeMapper.Map(schema.Items.First().Type);
			if (netType != null)
			{
				prop.Type = CollectionTypeHelper.GetCollectionType(netType);
			}
			else
			{
                prop.Type = CollectionTypeHelper.GetCollectionType(NetNamingMapper.GetObjectName(property.Key));
				foreach (var item in schema.Items)
				{
					ParseObject(property.Key, item.Properties, objects, enums);
				}
			}
		}

	    private void ParseProperties(IDictionary<string, ApiObject> objects, ICollection<Property> props, IDictionary<string, JsonSchema> properties, IDictionary<string, ApiEnum> enums)
		{
			if (properties == null)
				return;

			foreach (var property in properties)
			{
				if ( (property.Value.Enum != null && !property.Value.Enum.Any()) && (property.Value.Type == null || property.Value.Type == JsonSchemaType.Null 
                    || property.Value.Type == JsonSchemaType.None))
					continue;

				var key = property.Key;
				if (string.IsNullOrWhiteSpace(key))
					key = GetUniqueName(props);

			    var isEnum = property.Value.Enum != null && property.Value.Enum.Any();

			    var enumName = string.Empty;
                if (isEnum)
                {
                    enumName = ParseEnum(key, property.Value, enums, property.Value.Description);
                }

			    var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(key),
                               OriginalName = key,
                               Type = GetType(property, isEnum, enumName),
					           Description = property.Value.Description,
                               IsEnum = isEnum
				           };


				ParseComplexTypes(objects, property.Value, prop, property, key, enums);
				props.Add(prop);
			}
		}

        private string ParseEnum(string key, JsonSchema schema, IDictionary<string, ApiEnum> enums, string description)
        {
            var name = NetNamingMapper.GetObjectName(key);

            var apiEnum = new ApiEnum
            {
                Name = name,
                Description = description,
                Values = schema.Enum.Select(e => NetNamingMapper.GetEnumValueName(e.ToString())).ToList()
            };

            if (enums.ContainsKey(name))
            {
                if (IsAlreadyAdded(enums, apiEnum))
                    return name;

                apiEnum.Name = GetUniqueName(enums, name);
            }

            enums.Add(apiEnum.Name, apiEnum);

            return name;
        }

        private string ParseEnum(string key, Newtonsoft.JsonV4.Schema.JsonSchema schema, IDictionary<string, ApiEnum> enums, string description)
        {
            var name = NetNamingMapper.GetObjectName(key);

            var apiEnum = new ApiEnum
            {
                Name = name,
                Description = description,
                Values = schema.Enum.Select(e => NetNamingMapper.GetEnumValueName(e.ToString())).ToList()
            };

            if (enums.ContainsKey(name))
            {
                if(IsAlreadyAdded(enums, apiEnum))
                    return name;

                apiEnum.Name = GetUniqueName(enums, name);
            }

            enums.Add(apiEnum.Name, apiEnum);

            return apiEnum.Name;
        }

        private bool IsAlreadyAdded(IDictionary<string, ApiEnum> enums, ApiEnum apiEnum)
        {
            foreach (var @enum in enums)
            {
                if(apiEnum.Values.Count != @enum.Value.Values.Count)
                    continue;

                if (apiEnum.Values.Any(x => !@enum.Value.Values.Contains(x)))
                    continue;

                return true;
            }
            return false;
        }

        private string GetUniqueName(IDictionary<string, ApiEnum> enums, string name)
        {
            foreach (var suffix in suffixes)
            {
                var unique = name + suffix;
                if (enums.All(p => p.Key != unique))
                    return unique;               
            }
            for (var i = 0; i < 100; i++)
            {
                var unique = name + "A" + i;
                if (enums.All(p => p.Key != unique))
                    return unique;
            }
            throw new InvalidOperationException("Could not find a unique name for enum: " + name);
        }

        private string GetUniqueKey(IDictionary<string, ApiObject> objects, string key)
        {
            foreach (var suffix in suffixes)
            {
                var unique = key + suffix;
                if (objects.All(p => p.Key != unique) && otherObjects.All(p => p.Key != unique))
                    return unique;
            }
            for (var i = 0; i < 100; i++)
            {
                var unique = key + "A" + i;
                if (objects.All(p => p.Key != unique) && otherObjects.All(p => p.Key != unique))
                    return unique;
            }
            throw new InvalidOperationException("Could not find a key name for object: " + key);
        }

        private string GetUniqueName(IDictionary<string, ApiObject> objects, string name)
        {
            foreach (var suffix in suffixes)
            {
                var unique = name + suffix;
                if (objects.All(p => p.Value.Name != unique) && otherObjects.All(p => p.Value.Name != unique))
                    return unique;
            }
            for (var i = 0; i < 100; i++)
            {
                var unique = name + "A" + i;
                if (objects.All(p => p.Value.Name != unique) && otherObjects.All(p => p.Value.Name != unique))
                    return unique;
            }
            throw new InvalidOperationException("Could not find a unique name for object: " + name);
        }

        private string GetUniqueName(ICollection<Property> props)
		{
            foreach (var suffix in suffixes)
			{
				var unique = suffix;
				if (props.All(p => p.Name != unique))
					return unique;
			}
			for (var i = 0; i < 100; i++)
			{
				var unique = "A" + i;
				if (props.All(p => p.Name != unique))
					return unique;
			}
			throw new InvalidOperationException("Could not find a unique name for property");
		}

		private void ParseComplexTypes(IDictionary<string, ApiObject> objects, JsonSchema schema, Property prop, KeyValuePair<string, JsonSchema> property, string key, IDictionary<string, ApiEnum> enums)
		{
			if (schema.Type.HasValue && (schema.Type == JsonSchemaType.Object || schema.Type.Value.ToString().Contains("Object")) && schema.Properties != null)
			{
			    if (!string.IsNullOrWhiteSpace(schema.Id) && ids.Contains(schema.Id))
                        return;
			    
                if (!string.IsNullOrWhiteSpace(schema.Id))
                    ids.Add(schema.Id);

			    var type = string.IsNullOrWhiteSpace(property.Value.Id) ? key : property.Value.Id;
			    type = ParseObject(type, schema.Properties, objects, enums);
				prop.Type = NetNamingMapper.GetObjectName(type);
                return;
			}

		    if (schema.Type == JsonSchemaType.Array)
		        ParseArray(objects, schema, prop, property, enums);

		}

		private void ParseArray(IDictionary<string, ApiObject> objects, JsonSchema schema, Property prop, KeyValuePair<string, JsonSchema> property, IDictionary<string, ApiEnum> enums)
		{
			if (schema.Items == null || !schema.Items.Any())
				return;

			var netType = NetTypeMapper.Map(schema.Items.First().Type);
			if (netType != null)
			{
                prop.Type = CollectionTypeHelper.GetCollectionType(netType);
			}
			else
			{
                prop.Type = CollectionTypeHelper.GetCollectionType(NetNamingMapper.GetObjectName(property.Key));
				foreach (var item in schema.Items)
				{
					ParseObject(property.Key, item.Properties, objects, enums);
				}
			}
		}
	}
}