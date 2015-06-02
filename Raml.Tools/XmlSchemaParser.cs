using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Raml.Tools
{
    public class XmlSchemaParser
    {
        public ApiObject Parse(string schema, IDictionary<string, ApiObject> objects)
        {
            var codeNamespace = ConvertXml(schema);
            return ParseObjects(objects, codeNamespace);
        }

        private static ApiObject ParseObjects(IDictionary<string, ApiObject> objects, CodeNamespace codeNamespace)
        {
            ApiObject mainObject = null;
            foreach (CodeTypeDeclaration typeDeclaration in codeNamespace.Types)
            {
                var obj = new ApiObject {Name = typeDeclaration.Name};

                ParseProperties(typeDeclaration, obj);

                if (objects.ContainsKey(obj.Name) || objects.Any(o => o.Value.Name == obj.Name) || !obj.Properties.Any()) 
                    continue;
                
                objects.Add(obj.Name, obj);

                if (mainObject == null)
                    mainObject = obj;
            }
            return mainObject;
        }

        private static void ParseProperties(CodeTypeDeclaration typeDeclaration, ApiObject obj)
        {
            foreach (CodeTypeMember property in typeDeclaration.Members)
            {
                ParseProperty(property, obj);
            }
        }

        private static void ParseProperty(CodeTypeMember property, ApiObject obj)
        {
            var memberProperty = property as CodeMemberProperty;
            if (memberProperty != null)
            {
                var prop = new Property
                {
                    Name = memberProperty.Name,
                    Type = (memberProperty.Type.ArrayRank == 1)
                        ? CollectionTypeHelper.GetCollectionType(memberProperty.Type.BaseType)
                        : memberProperty.Type.BaseType
                };
                obj.Properties.Add(prop);
            }
        }

        private static CodeNamespace ConvertXml(string schema)
        {
            var xsd = ReadSchema(schema);
            var maps = ImportXmlTypeMappings(xsd);

            var codeNamespace = ExportTypeMappings(maps);

            return codeNamespace;
        }

        private static XmlSchema ReadSchema(string schema)
        {
            XmlSchema xsd;
            using (var stream = new StringReader(schema))
            {
                xsd = XmlSchema.Read(stream, null);
            }
            return xsd;
        }

        private static CodeNamespace ExportTypeMappings(IEnumerable<XmlTypeMapping> maps)
        {
            var codeNamespace = new CodeNamespace("Generated");
            var codeExporter = new XmlCodeExporter(codeNamespace);
            foreach (var map in maps)
            {
                codeExporter.ExportTypeMapping(map);
            }
            return codeNamespace;
        }

        private static IEnumerable<XmlTypeMapping> ImportXmlTypeMappings(XmlSchema xsd)
        {
            var xsds = new XmlSchemas { xsd };
            xsds.Compile(null, true);

            var schemaImporter = new XmlSchemaImporter(xsds);
            var maps = new List<XmlTypeMapping>();
            maps.AddRange(ImportSchemaTypes(xsd, schemaImporter));
            maps.AddRange(ImportTypeMappings(xsd, schemaImporter));
            return maps;
        }

        private static IEnumerable<XmlTypeMapping> ImportTypeMappings(XmlSchema xsd, XmlSchemaImporter schemaImporter)
        {
            var maps = new Collection<XmlTypeMapping>();
            foreach (XmlSchemaElement schemaElement in xsd.Elements.Values)
            {
                var importTypeMapping = schemaImporter.ImportTypeMapping(schemaElement.QualifiedName);
                maps.Add(importTypeMapping);
            }
            return maps;
        }

        private static IEnumerable<XmlTypeMapping> ImportSchemaTypes(XmlSchema xsd, XmlSchemaImporter schemaImporter)
        {
            var maps = new Collection<XmlTypeMapping>();
            foreach (XmlSchemaType schemaType in xsd.SchemaTypes.Values)
            {
                var importSchemaType = schemaImporter.ImportSchemaType(schemaType.QualifiedName);
                maps.Add(importSchemaType);
            }
            return maps;
        }
    }
}