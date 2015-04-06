using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class JsonSchemaParserTests
	{
		[Test]
		public void should_parse_schema_when_object()
		 {
			 const string schema = "{\r\n" +
			                       "      \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
			                       "      \"type\": \"object\",\r\n" +
			                       "      \"properties\": \r\n" +
			                       "      {\r\n" +
			                       "        \"id\": { \"type\": \"integer\", \"required\": true },\r\n" +
			                       "        \"at\": { \"type\": \"string\", \"required\": true },\r\n" +
			                       "        \"toAddressId\": { \"type\": \"string\", \"required\": true },\r\n" +
			                       "        \"orderItemId\": { \"type\": \"string\", \"required\": true },\r\n" +
			                       "        \"status\": { \"type\": \"string\", \"required\": true, \"enum\": [ \"scheduled\", \"completed\", \"failed\" ] },\r\n" +
			                       "        \"droneId\": { \"type\": \"string\" }\r\n" +
			                       "      }\r\n" +
			                       "    }\r\n";
			 var parser = new JsonSchemaParser();
			 var warnings = new Dictionary<string, string>();
			 var objects = new Dictionary<string, ApiObject>();
			 var obj = parser.Parse("name", schema, objects, warnings);
			 Assert.AreEqual(0, warnings.Count);
			 Assert.AreEqual("Name", obj.Name);
			 Assert.IsFalse(obj.IsArray);
			 Assert.AreEqual(6, obj.Properties.Count);
			 Assert.AreEqual("int", obj.Properties.First(p => p.Name == "Id").Type);
		 }

		[Test]
		public void should_parse_schema_when_array()
		{
			const string schema = "{\r\n" +
			                      "  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
			                      "  \"type\": \"array\",\r\n" +
			                      "  \"items\": \r\n" +
			                      "  {\r\n" +
			                      "    \"type\": \"object\",\r\n" +
			                      "    \"properties\": \r\n" +
			                      "    {\r\n" +
			                      "      \"id\": { \"type\": \"string\", \"required\": true },\r\n" +
			                      "      \"at\": { \"type\": \"string\", \"required\": true },\r\n" +
			                      "      \"toAddressId\": { \"type\": \"integer\", \"required\": true },\r\n" +
			                      "      \"orderItemId\": { \"type\": \"string\", \"required\": true },\r\n" +
			                      "      \"status\": { \"type\": \"string\", \"required\": true, \"enum\": [ \"scheduled\", \"completed\", \"failed\" ] },\r\n" +
			                      "      \"droneId\": { \"type\": \"string\" }\r\n" +
			                      "    }\r\n" +
			                      "  }\r\n" +
			                      "}\r\n";
			var parser = new JsonSchemaParser();
			var warnings = new Dictionary<string, string>();
			var objects = new Dictionary<string, ApiObject>();
			var obj = parser.Parse("name", schema, objects, warnings);
			Assert.AreEqual(0, warnings.Count);
			Assert.AreEqual("Name", obj.Name);
			Assert.IsTrue(obj.IsArray);
			Assert.AreEqual(6, obj.Properties.Count);
			Assert.AreEqual("int", obj.Properties.First(p => p.Name == "ToAddressId").Type);
		}

        [Test]
        public void should_keep_original_names()
        {
            const string schema = "{\r\n" +
                                  "  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                                  "  \"type\": \"array\",\r\n" +
                                  "  \"items\": \r\n" +
                                  "  {\r\n" +
                                  "    \"type\": \"object\",\r\n" +
                                  "    \"properties\": \r\n" +
                                  "    {\r\n" +
                                  "      \"id\": { \"type\": \"string\", \"required\": true },\r\n" +
                                  "      \"at\": { \"type\": \"string\", \"required\": true },\r\n" +
                                  "      \"to-address-id\": { \"type\": \"integer\", \"required\": true },\r\n" +
                                  "      \"order_item_id\": { \"type\": \"string\", \"required\": true },\r\n" +
                                  "      \"status\": { \"type\": \"string\", \"required\": true, \"enum\": [ \"scheduled\", \"completed\", \"failed\" ] },\r\n" +
                                  "      \"droneId\": { \"type\": \"string\" }\r\n" +
                                  "    }\r\n" +
                                  "  }\r\n" +
                                  "}\r\n";
            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var obj = parser.Parse("name", schema, objects, warnings);
            Assert.AreEqual("to-address-id", obj.Properties.First(p => p.Name == "ToAddressId").OriginalName);
            Assert.AreEqual("order_item_id", obj.Properties.First(p => p.Name == "OrderItemId").OriginalName);
        }
	}
}