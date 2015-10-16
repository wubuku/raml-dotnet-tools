using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;

namespace Raml.Tools
{
    [Serializable]
    public class Property
    {
        private readonly string[] reservedWords = { "ref", "out", "in", "base", "long", "int", "short", "bool", "string", "decimal", "float", "double" };
        private string name;

        

        public string Name
        {
            get
            {
                if (reservedWords.Contains(name.ToLowerInvariant()))
                    return "Ip" + name.ToLowerInvariant();

                return name;
            }

            set { name = value; }
        }

        public string OriginalName { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Example { get; set; }
        public bool Required { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public string JSONSchema { get; set; }
        public bool IsEnum { get; set; }

        public bool IsAdditionalProperties
        {
            get { return Name == "AdditionalProperties" && Type == "IDictionary<string, object>"; }
        }

        public int? MaxLength { get; set; }
        public int? MinLength { get; set; }

        public string CustomAttributes
        {
            get
            {
                var attributes = new Collection<string>();

                var identation = "".PadLeft(8);

                if (Required)
                    attributes.Add("[Required]".Insert(0, identation));

                if (MaxLength != null)
                    attributes.Add(string.Format("[MaxLength({0})]", MaxLength).Insert(0, identation));

                if (MinLength != null)
                    attributes.Add(string.Format("[MinLength({0})]", MinLength).Insert(0, identation));

                if (Minimum != null || Maximum != null)
                    BuildRangeAttribute(attributes, identation);

                return string.Join(Environment.NewLine, attributes);
            }
        }

        private void BuildRangeAttribute(Collection<string> attributes, string identation)
        {
            if (Type == "int")
            {
                attributes.Add(string.Format("[Range({0:F0},{1:F0})]", Minimum ?? int.MinValue, Maximum ?? int.MaxValue)
                    .Insert(0, identation));
            }
            else
            {
                var culture = new CultureInfo("en-US");
                var min = Minimum ?? double.MinValue;
                var max = Maximum ?? double.MaxValue;
                attributes.Add(string.Format("[Range({0},{1})]", min.ToString("F", culture), max.ToString("F", culture))
                    .Insert(0, identation));
            }
        }

        public double? Maximum { get; set; }
        public double? Minimum { get; set; }
    }
}