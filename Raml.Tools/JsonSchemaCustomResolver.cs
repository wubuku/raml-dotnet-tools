using System.Collections.Generic;
using Newtonsoft.Json.Schema;

namespace Raml.Tools
{
    public class JsonSchemaCustomResolver : JsonSchemaResolver
    {
        private readonly IDictionary<string, ApiObject> objects;

        public JsonSchemaCustomResolver(IDictionary<string, ApiObject> objects)
        {
            this.objects = objects;
        }

        public override JsonSchema GetSchema(string reference)
        {
            var schema = base.GetSchema(reference);

            if (schema != null) return schema;

            if (!objects.ContainsKey(reference))
                return null;

            var jsonSchema = objects[reference].JSONSchema.Replace("\\\"", "\"");
            return JsonSchema.Parse(jsonSchema, new JsonSchemaCustomResolver(objects));
        }
    }
}