
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
        public static async Task ValidateWithExceptionAsync(string rawSchema, HttpContent content)
        {
            var isValid = await IsValidAsync(rawSchema, content);
            if (!isValid)
            {
                throw new SchemaValidationException("The response is not valid according to the associated schema");
            }
        }

        public static async Task<bool> IsValidAsync(string rawSchema, HttpContent content)
        {
            if (!content.Headers.ContentType.MediaType.Equals("application/json",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            var rawContent = await content.ReadAsStringAsync();
            return IsValidJSON(rawSchema, rawContent);
        }

        public static bool IsValid(string rawSchema, HttpContent content)
        {
            if (!content.Headers.ContentType.MediaType.Equals("application/json",
                StringComparison.InvariantCultureIgnoreCase))
            {
                return true;
            }

            var readTask = content.ReadAsStringAsync().ConfigureAwait(false);
            var rawResponse = readTask.GetAwaiter().GetResult();

            return IsValidJSON(rawSchema, rawResponse);

        }

        private static bool IsValidJSON(string rawSchema, string responseString)
        {
            JsonSchema schema;
            v4SchemaNS.JsonSchema v4Schema;
            
            JToken data = null;
            if (responseString.StartsWith("["))
                data = JArray.Parse(responseString);
            else
                data = JObject.Parse(responseString);

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
                    
                    v4LinqNS.JToken datav4 = null;
                    if (responseString.StartsWith("["))
                        datav4 = v4LinqNS.JArray.Parse(responseString);
                    else
                        datav4 = v4LinqNS.JObject.Parse(responseString);
                                        
                    if (!v4SchemaNS.Extensions.IsValid(datav4, v4Schema))
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
