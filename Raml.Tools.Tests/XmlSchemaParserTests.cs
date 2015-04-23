using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using NUnit.Framework;

namespace Raml.Tools.Tests
{
    [TestFixture]
    public class XmlSchemaParserTests
    {
        private static XmlSchemaParser parser;

        [SetUp]
        public void SetUp()
        {
            parser = new XmlSchemaParser();
        }

        [Test]
        public void should_parse_and_return_object()
        {
            var schema = string.Empty;
            var apiObjects = new Dictionary<string, ApiObject>();
            var apiObject = parser.Parse("key", schema, apiObjects);
            Assert.IsNotNull(apiObject);
        }

        [Test]
        public void should_name_object()
        {
            var schema = string.Empty;
            var apiObjects = new Dictionary<string, ApiObject>();
            var apiObject = parser.Parse("key", schema, apiObjects);
            Assert.AreEqual("Key", apiObject.Name);
        }

        [Test]
        public void should_not_add_object_to_collection_when_no_properties()
        {
            var schema = string.Empty;
            var apiObjects = new Dictionary<string, ApiObject>();
            parser.Parse("key", schema, apiObjects);
            Assert.AreEqual(0, apiObjects.Count);
        }

        [Test]
        public void should_avoid_key_duplication()
        {
            var schema = string.Empty;
            var apiObjects = new Dictionary<string, ApiObject>
            {
                { "key", new ApiObject() }
            };
            parser.Parse("key", schema, apiObjects);
            Assert.AreEqual(1, apiObjects.Count);
        }

        [Test]
        public void should_avoid_name_duplication()
        {
            var schema = string.Empty;
            var apiObjects = new Dictionary<string, ApiObject>
            {
                { "Key", new ApiObject() }
            };
            parser.Parse("key", schema, apiObjects);
            Assert.AreEqual(1, apiObjects.Count);
        }

        [Test]
        public void should_parse_properties()
        {
            var schema = File.ReadAllText(@"C:\desarrollo\mulesoft\xmlschema2006-11-06\boeingData\ipo1\ipo.xsd");

            var apiObject = parser.Parse("key", schema, new Dictionary<string, ApiObject>());

            Assert.AreEqual(5, apiObject.Properties.Count);
        }




    }
}
