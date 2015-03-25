using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class JsonSchemaParserTests
	{
        [Test]
        public void should_parse_v4_schema()
        {
           
            var schema = @"{
          'id': 'http://some.site.somewhere/entry-schema#',
          '$schema': 'http://json-schema.org/draft-04/schema#',
          'description': 'schema for an fstab entry',
          'type': 'object',
          'required': [ 'storage' ],
          'definitions': {
              'diskDevice': {
                  'properties': {
                      'type': { 'enum': [ 'disk' ] },
                      'device': {
                          'type': 'string',
                          'pattern': '^/dev/[^/]+(/[^/]+)*$'
                      }
                  },
                  'required': [ 'type', 'device' ],
                  'additionalProperties': false
              },
              'diskUUID': {
                  'properties': {
                      'type': { 'enum': [ 'disk' ] },
                      'label': {
                          'type': 'string',
                          'pattern': '^[a-fA-F0-9]{8}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{4}-[a-fA-F0-9]{12}$'
                      }
                  },
                  'required': [ 'type', 'label' ],
                  'additionalProperties': false
              },
              'nfs': {
                  'properties': {
                      'type': { 'enum': [ 'nfs' ] },
                      'remotePath': {
                          'type': 'string',
                          'pattern': '^(/[^/]+)+$'
                      },
                      'server': {
                          'type': 'string',
                          'oneOf': [
                              { 'format': 'host-name' },
                              { 'format': 'ipv4' },
                              { 'format': 'ipv6' }
                          ]
                      }
                  },
                  'required': [ 'type', 'server', 'remotePath' ],
                  'additionalProperties': false
              },
              'tmpfs': {
                  'properties': {
                      'type': { 'enum': [ 'tmpfs' ] },
                      'sizeInMB': {
                          'type': 'integer',
                          'minimum': 16,
                          'maximum': 512
                      }
                  },
                  'required': [ 'type', 'sizeInMB' ],
                  'additionalProperties': false
              }
          },
          'properties': {
              'storage': {
                  'type': 'object',
                  'oneOf': [
                      { '$ref': '#/definitions/diskDevice' },
                      { '$ref': '#/definitions/diskUUID' },
                      { '$ref': '#/definitions/nfs' },
                      { '$ref': '#/definitions/tmpfs' }
                  ]
              },
              'fstype': {
                  'enum': [ 'ext3', 'ext4', 'btrfs' ]
              },
              'options': {
                  'type': 'array',
                  'minItems': 1,
                  'items': { 'type': 'string' },
                  'uniqueItems': true
              },
              'readonly': { 'type': 'boolean' }
          },
          
      }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var obj = parser.Parse("name", schema, objects, warnings);

            Assert.AreEqual(0, warnings.Count);
            Assert.AreEqual("Name", obj.Name);

        }

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
	}
}