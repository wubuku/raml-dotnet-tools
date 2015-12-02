using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Schema;
using Raml.Tools.WebApiGenerator;

namespace Raml.Tools
{
    [Serializable]
    public class ApiObject : IHasName
    {
        public ApiObject()
        {
            Properties = new List<Property>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Example { get; set; }
        public IList<Property> Properties { get; set; }
        public bool IsArray { get; set; }
        public bool IsMultiple { get; set; }
        public string JSONSchema { get; set; }

	    public string GeneratedCode { get; set; }

        public string BaseClass { get; set; }

        public string ConstructorParams
        {
            get
            {
                var res = string.Empty;
                if (Properties.Any(p => p.Name == "UriParameters"))
                {
                    var uriParams = Properties.First(p => p.Name == "UriParameters");
                    res += uriParams.Type + " " + uriParams.Name;
                }

                var paramStrings = Properties
                    .Where(p => p != null && p.Type != null && p.Name != null && p.Name != "UriParameters")
                    .Select(p => p.Type + " " + p.Name + " = null").ToArray();

                if (!paramStrings.Any())
                    return res;

                return res + (string.IsNullOrWhiteSpace(res) ? "" : ", ") + string.Join(", ", paramStrings);
            }
        }

        public string Type { get; set; }
        public bool IsScalar { get; set; }
    }
}