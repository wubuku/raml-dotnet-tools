
using Newtonsoft.Json.Schema;
using v4SchemaNS = Newtonsoft.JsonV4.Schema;
using v4LinqNS = Newtonsoft.JsonV4.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace RAML.Api.Core
{
    public static class SchemaValidator
    {
        public static void ValidateWithExceptions(string rawSchema, HttpContent content)
        {
            if (!IsValidJSON(rawSchema, content))
            {
                throw new SchemaValidationException("The response failed to validate against the schema.");
            }
        }

        public static bool IsValidJSON(string rawSchema, HttpContent content)
        {
            var stringReader = content.ReadAsStringAsync();
            stringReader.Wait();

            var rawResponse = stringReader.Result;
            return IsValidJSON(rawSchema, rawResponse);
        }

        public static bool IsValidJSON(string rawSchema, string responseString)
        {
            JsonSchema schema;
            v4SchemaNS.JsonSchema v4Schema;
            var data = JObject.Parse(responseString);

            try
            {
                schema = JsonSchema.Parse(rawSchema);

                if (!data.IsValid(schema))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                try
                {
                    v4Schema = v4SchemaNS.JsonSchema.Parse(rawSchema);
                    var datav4 = v4LinqNS.JObject.Parse(responseString);

                    if (!v4SchemaNS.Extensions.IsValid(datav4, v4Schema))
                    {
                        return false;
                    }
                }
                catch (Exception exv4)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
