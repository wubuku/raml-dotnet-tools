using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Schema;

namespace Raml.Tools
{
    public class CollectionTypeHelper
    {
        public const string CollectionType = "ICollection";

        public static string GetCollectionType(string netType)
        {
            return CollectionType + "<" + netType + ">";
        }

        public static string GetBaseReturnType(string type)
        {
            if (!type.StartsWith(CollectionTypeHelper.CollectionType)) return type;

            type = type.Replace(CollectionTypeHelper.CollectionType, string.Empty);
            type = type.Substring(1, type.Length - 2);
            return type;
        }
    }

	public class JsonSchemaParser
	{
	    
	    private readonly string[] suffixes = { "A", "B", "C", "D", "E", "F", "G" };

		public ApiObject Parse(string key, string jsonSchema, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings)
		{
			var obj = new ApiObject
			          {
				          Name = NetNamingMapper.GetObjectName(key),
				          Properties = new List<Property>()
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
						ParseProperties(objects, obj.Properties, schema.Items.First().Properties);
				}
				else
				{
					ParseProperties(objects, obj.Properties, schema.Properties);
				}
			}
			else
			{
				if (v4Schema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Array)
				{
					obj.IsArray = true;
					if (v4Schema.Items != null && v4Schema.Items.Any())
						ParseProperties(objects, obj.Properties, v4Schema.Items.First().Properties);
				}
				else
				{
					ParseProperties(objects, obj.Properties, v4Schema.Properties);
				}
			}
			return obj;
		}

		private void ParseObject(string key, IDictionary<string, JsonSchema> schema, IDictionary<string, ApiObject> objects)
		{
			if (schema == null)
				return;

			var obj = new ApiObject
			{
				Name = NetNamingMapper.GetObjectName(key),
				Properties = ParseSchema(schema, objects)
			};

			// Avoid duplicated keys and names
			if (objects.ContainsKey(key) || objects.Any(o => o.Value.Name == obj.Name) || !obj.Properties.Any())
				return;

			objects.Add(key, obj);
		}


		private IList<Property> ParseSchema(IDictionary<string, JsonSchema> schema, IDictionary<string, ApiObject> objects)
		{
			var props = new List<Property>();
			foreach (var kv in schema)
			{
				var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(kv.Key),
                               OriginalName = kv.Key,
					           Type = NetTypeMapper.Map(kv.Value.Type),
					           Description = kv.Value.Description
				           };
				ParseComplexTypes(objects, kv.Value, prop, kv, kv.Key);
				props.Add(prop);
			}
			return props;
		}


		private void ParseProperties(IDictionary<string, ApiObject> objects, IList<Property> props, IDictionary<string, Newtonsoft.JsonV4.Schema.JsonSchema> properties)
		{
			foreach (var property in properties)
			{
				if (property.Value.Type == null || property.Value.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Null ||
				    property.Value.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.None)
					continue;

				var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(property.Key),
                               OriginalName = property.Key,
					           Type = NetTypeMapper.Map(property.Value.Type),
					           Description = property.Value.Description
				           };

				ParseComplexTypes(objects, property.Value, prop, property);
				props.Add(prop);
			}
		}

		private void ParseComplexTypes(IDictionary<string, ApiObject> objects, Newtonsoft.JsonV4.Schema.JsonSchema schema, Property prop, KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property)
		{
			if (schema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Object && schema.Properties != null)
			{
				ParseObject(property.Key, schema.Properties, objects);
				prop.Type = NetNamingMapper.GetObjectName(property.Key);
			}

			if (schema.Type == Newtonsoft.JsonV4.Schema.JsonSchemaType.Array)
				ParseArray(objects, schema, prop, property);
		}

		private void ParseObject(string key, IDictionary<string, Newtonsoft.JsonV4.Schema.JsonSchema> schema, IDictionary<string, ApiObject> objects)
		{
			var obj = new ApiObject
			          {
				          Name = NetNamingMapper.GetObjectName(key),
				          Properties = ParseSchema(schema, objects)
			          };

			// Avoid duplicated keys and names
			if (objects.ContainsKey(key) || objects.Any(o => o.Value.Name == obj.Name) || !obj.Properties.Any())
				return;

			objects.Add(key, obj);
		}

		private IList<Property> ParseSchema(IDictionary<string, Newtonsoft.JsonV4.Schema.JsonSchema> schema, IDictionary<string, ApiObject> objects)
		{
			var props = new List<Property>();
			foreach (var kv in schema)
			{
				var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(kv.Key),
                               OriginalName = kv.Key,
					           Type = NetTypeMapper.Map(kv.Value.Type),
					           Description = kv.Value.Description
				           };
				ParseComplexTypes(objects, kv.Value, prop, kv);
				props.Add(prop);
			}
			return props;
		}

		private void ParseArray(IDictionary<string, ApiObject> objects, Newtonsoft.JsonV4.Schema.JsonSchema schema, Property prop, KeyValuePair<string, Newtonsoft.JsonV4.Schema.JsonSchema> property)
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
					ParseObject(property.Key, item.Properties, objects);
				}
			}
		}

	    private void ParseProperties(IDictionary<string, ApiObject> objects, ICollection<Property> props, IDictionary<string, JsonSchema> properties)
		{
			if (properties == null)
				return;

			foreach (var property in properties)
			{
				if (property.Value.Type == null || property.Value.Type == JsonSchemaType.Null ||
				    property.Value.Type == JsonSchemaType.None)
					continue;

				var key = property.Key;
				if (string.IsNullOrWhiteSpace(key))
					key = GetUniqueName(props);

				var prop = new Property
				           {
					           Name = NetNamingMapper.GetPropertyName(key),
                               OriginalName = key,
					           Type = NetTypeMapper.Map(property.Value.Type),
					           Description = property.Value.Description
				           };

				ParseComplexTypes(objects, property.Value, prop, property, key);
				props.Add(prop);
			}
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

		private void ParseComplexTypes(IDictionary<string, ApiObject> objects, JsonSchema schema, Property prop, KeyValuePair<string, JsonSchema> property, string key)
		{
			if (schema.Type == JsonSchemaType.Object && schema.Properties != null)
			{
				ParseObject(key, schema.Properties, objects);
				prop.Type = NetNamingMapper.GetObjectName(key);
			}

			if (schema.Type == JsonSchemaType.Array)
				ParseArray(objects, schema, prop, property);
		}

		private void ParseArray(IDictionary<string, ApiObject> objects, JsonSchema schema, Property prop, KeyValuePair<string, JsonSchema> property)
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
					ParseObject(property.Key, item.Properties, objects);
				}
			}
		}
	}
}