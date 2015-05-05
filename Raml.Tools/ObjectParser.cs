using System.Collections.Generic;

namespace Raml.Tools
{
	public class ObjectParser
	{
		private readonly JsonSchemaParser jsonSchemaParser = new JsonSchemaParser();

		public ApiObject ParseObject(string key, string value, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings, IDictionary<string, ApiEnum> enums)
		{
			var obj = ParseSchema(key, value, objects, warnings, enums);
			if (obj == null)
				return null;

			obj.Name = NetNamingMapper.GetObjectName(key);

			return obj;
		}

		private ApiObject ParseSchema(string key, string schema, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings, IDictionary<string, ApiEnum> enums)
		{
			if (schema == null)
				return null;

			// is a reference, should then be defined elsewhere
			if (schema.Contains("<<") && schema.Contains(">>"))
				return null;

			if (schema.Trim().StartsWith("<"))
				return ParseXmlSchema(key, schema, objects);

			return jsonSchemaParser.Parse(key, schema, objects, warnings, enums);
		}



		// TODO
		private ApiObject ParseXmlSchema(string key, string schema, IDictionary<string, ApiObject> objects)
		{
			throw new System.NotImplementedException("XML Schema Parsing not implemented - " + key);
		}

	}
}