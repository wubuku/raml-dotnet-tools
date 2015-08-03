using System.Collections.Generic;
using System.Linq;
using Raml.Common;

namespace Raml.Tools
{
    public class ObjectParser
    {
        private readonly JsonSchemaParser jsonSchemaParser = new JsonSchemaParser();

        public ApiObject ParseObject(string key, string value, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings, IDictionary<string, ApiEnum> enums, IDictionary<string, ApiObject> otherObjects, IDictionary<string, ApiObject> schemaObjects, string targetNamespace)
        {
            var obj = ParseSchema(key, value, objects, warnings, enums, otherObjects, schemaObjects, targetNamespace);
            if (obj == null)
                return null;

            obj.Name = NetNamingMapper.GetObjectName(key);

            if (schemaObjects.Values.Any(o => o.Name == obj.Name) || objects.Values.Any(o => o.Name == obj.Name) ||
                otherObjects.Values.Any(o => o.Name == obj.Name))
            {
                if (UniquenessHelper.HasSameProperties(obj, objects, key, otherObjects, schemaObjects))
                    return null;
            }

            return obj;
        }

        private ApiObject ParseSchema(string key, string schema, IDictionary<string, ApiObject> objects, IDictionary<string, string> warnings, IDictionary<string, ApiEnum> enums, IDictionary<string, ApiObject> otherObjects, IDictionary<string, ApiObject> schemaObjects, string targetNamespace)
		{
   			if (schema == null)
				return null;

            // is a reference, should then be defined elsewhere
            if (schema.Contains("<<") && schema.Contains(">>"))
                return null;

            if (schema.Trim().StartsWith("<"))
                return ParseXmlSchema(key, schema, objects, targetNamespace);

            if (!schema.Contains("{"))
                return null;

            return jsonSchemaParser.Parse(key, schema, objects, warnings, enums, otherObjects, schemaObjects);
        }

		private ApiObject ParseXmlSchema(string key, string schema, IDictionary<string, ApiObject> objects, string targetNamespace)
		{
            if(objects.ContainsKey(key))
                return null;

		    var xmlSchemaParser = new XmlSchemaParser();
            var  obj = xmlSchemaParser.Parse(key, schema, objects, targetNamespace);

            if (obj != null && !objects.ContainsKey(key))
		        objects.Add(key, obj); // to associate that key with the main XML Schema object

		    return obj;
		}

    }
}