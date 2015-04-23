using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Schema;

namespace Raml.Tools
{
    public class XmlSchemaParser
    {
        public ApiObject Parse(string key, string schema, IDictionary<string, ApiObject> objects)
        {
            var apiObject = new ApiObject
            {
                Name = NetNamingMapper.GetObjectName(key)
            };

            if (!objects.ContainsKey(key) && objects.All(o => o.Value.Name != apiObject.Name) && apiObject.Properties.Any())
                objects.Add(key, apiObject);

            var reader = new XmlTextReader(new StringReader(schema));
            //reader.SchemaInfo.SchemaElement
            var xmlSchema = XmlSchema.Read(reader, ValidationCallback);

            apiObject.Properties = ParseProperties(xmlSchema.Items).ToList();

            return apiObject;
        }

        private IEnumerable<Property> ParseProperties(XmlSchemaObjectCollection items)
        {
            var properties = new List<Property>();

            foreach (var item in items)
            {
                var i = item.LineNumber;
                if (item is XmlSchemaElement)
                {
                    var element = ((XmlSchemaElement)item);

                    var prop = new Property
                    {
                        OriginalName = element.Name,
                        Name = NetNamingMapper.GetPropertyName(element.Name),
                        Type = NetTypeMapper.Map(element.SchemaTypeName),
                    };

                    properties.Add(prop);
                }
                else if (item is XmlSchemaComplexType)
                {
                    var element = ((XmlSchemaComplexType)item);

                    var prop = new Property
                    {
                        OriginalName = element.Name,
                        Name = NetNamingMapper.GetPropertyName(element.Name),
                        Type = element.Name,
                    };

                    properties.Add(prop);
                }
                else if (item is XmlSchemaSimpleType)
                {
                    var element = ((XmlSchemaSimpleType)item);
                    var name = element.Name;
                    var tpe = element.BaseXmlSchemaType;
                    var tpe2 = element.Content;
                    var parent = element.Parent;

                    var prop = new Property
                    {
                        OriginalName = element.Name,
                        Name = NetNamingMapper.GetPropertyName(element.Name),
                        Type = element.Name,
                    };

                    properties.Add(prop);
                }
                var a = 1;
            }
            return properties;
        }

        static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);
        }


    }
}