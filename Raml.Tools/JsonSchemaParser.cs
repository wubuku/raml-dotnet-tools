using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Schema;
using Raml.Common;

namespace Raml.Tools
{
    public class JsonSchemaParser
	{
	    
	    private readonly string[] suffixes = { "A", "B", "C", "D", "E", "F", "G" };
        private readonly ICollection<string> ids  = new Collection<string>();

		public ApiObject Parse(string key, string jsonSchema, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings, IDictionary<string, ApiEnum> enums)
		{
			var obj = new ApiObject
			          {
				          Name = NetNamingMapper.GetObjectName(key),
				          Properties = new List<Property>(),
                          JSONSchema = jsonSchema.Replace(Environment.NewLine, "").Replace("\r\n", "").Replace("\n", "").Replace("\"", "\\\"")
			          };
			JsonSchema schema = null;
			Newtonsoft.JsonV4.Schema.JsonSchema v4Schema = null;
			try
			{
				schema = JsonSchema.Parse(jsonSchema);
			}
			catch (Exception exv3) // NewtonJson does not support Json Schema v4
			{
				try
				{
					schema = null;
					v4Schema = Newtonsoft.JsonV4.Schema.JsonSchema.Parse(jsonSchema);
				}
				catch (Exception exv4)
				{
					//dynamic dynObj = JsonConvert.DeserializeObject(jsonSchema);
					//foreach (var kv in dynObj)
					//{
					//	//TODO: manual parse schema ? or parse using example ?
					//}
					if (!warnings.ContainsKey(key))
						warnings.Add(key, "Could not parse JSON Schema. v3 parser message: " + exv3.Message.Replace("\r\n", string.Empty).Replace("\n", string.Empty) + ". v4 parser message: " + exv4.Message.Replace("\r\n", string.Empty).Replace("\n", string.Empty));
				}
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

		private void ParseObject(string key, IDictionary<string, JsonSchema> schema, IDictionary<string, ApiObject> objects, IDictionary<string, ApiEnum> enums)
		{
			if (schema == null)
				return;

			var obj = new ApiObject
			{
				Name = NetNamingMapper.GetObjectName(key),
				Properties = ParseSchema(schema, objects, enums)
			};

			// Avoid duplicated keys and names
			if (objects.ContainsKey(key) || objects.Any(o => o.Value.Name == obj.Name) || !obj.Properties.Any())
				return;

			objects.Add(key, obj);
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
            
            if(!string.IsNullOrWhiteSpace(NetTypeMapper.Map(property.Value.Type)))
                return NetTypeMapper.Map(property.Value.Type);

            if (!string.IsNullOrWhiteSpace(property.Value.Id))
                return NetNamingMapper.GetObjectName(property.Value.Id);

            return NetNamingMapper.GetObjectName(property.Key);
        }

        private static string GetType(KeyValuePair<string, JsonSchema> property, bool isEnum, string enumName)
        {
            if (isEnum)
                return enumName;

            if (!string.IsNullOrWhiteSpace(NetTypeMapper.Map(property.Value.Type)))
                return NetTypeMapper.Map(property.Value.Type);

            if (!string.IsNullOrWhiteSpace(property.Value.Id))
                return NetNamingMapper.GetObjectName(property.Value.Id);

            return NetNamingMapper.GetObjectName(property.Key);
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
            for (var i = 0; i < 9; i++)
            {
                var unique = name + suffixes[i];
                if (enums.All(p => p.Key != unique))
                    return unique;
            }
            for (var i = 0; i < 100; i++)
            {
                var unique = name + "A" + i;
                if (enums.All(p => p.Key != unique))
                    return unique;
            }
            throw new InvalidOperationException("Could not find a unique name for enum");
        }

        private string GetUniqueName(ICollection<Property> props)
		{
			for (var i = 0; i < 7; i++)
			{
				var unique = suffixes[i];
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
			    ParseObject(type, schema.Properties, objects, enums);
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