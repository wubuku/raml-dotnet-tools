using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(0, warnings.Count);
            Assert.AreEqual("Name", obj.Name);
        }

        [Test]
        public void should_parse_enums()
        {
            const string schema = @"{
          'id': 'http://some.site.somewhere/entry-schema#',
          '$schema': 'http://json-schema.org/draft-03/schema#',
          'description': 'schema for an fstab entry',
          'type': 'object',
          'properties': {
              'fstype': {
                  'enum': [ 'ext3', 'ext4', 'btrfs' ]
              },
              'readonly': { 'type': 'boolean' }
          },
      }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(0, warnings.Count);
            Assert.AreEqual(2, obj.Properties.Count);
            Assert.AreEqual(1, enums.Count);
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
             var enums = new Dictionary<string, ApiEnum>();
             var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());
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
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());
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
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());
            Assert.AreEqual("to-address-id", obj.Properties.First(p => p.Name == "Toaddressid").OriginalName);
            Assert.AreEqual("order_item_id", obj.Properties.First(p => p.Name == "Order_item_id").OriginalName);
        }

        [Test]
        public void should_parse_recursive_schemas()
        {
            var schema = "      { \r\n" +
                         "        \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                         "        \"type\": \"object\",\r\n" +
                         "        \"id\": \"Customer\",\r\n" +
                         "        \"properties\": {\r\n" +
                         "          \"Id\": { \"type\": \"integer\"},\r\n" +
                         "          \"Company\": { \"type\": \"string\"},\r\n" +
                         "          \"SupportRepresentant\":\r\n" +
                         "            { \r\n" +
                         "              \"type\": \"object\",\r\n" +
                         "              \"id\": \"Employee\",\r\n" +
                         "              \"properties\": {\r\n" +
                         "                \"Id\": { \"type\": \"integer\"},\r\n" +
                         "                \"Title\": { \"type\": \"string\"},\r\n" +
                         "                \"BirthDate\": { \"type\": \"string\"},\r\n" +
                         "                \"HireDate\": { \"type\": \"string\"},\r\n" +
                         "                \"ReportsTo\":\r\n" +
                         "                  { \"$ref\": \"Employee\" },\r\n" +
                         "                \"FirstName\": { \"type\": \"string\"},\r\n" +
                         "                \"LastName\": { \"type\": \"string\"},\r\n" +
                         "                \"Address\": { \"type\": \"string\"},\r\n" +
                         "                \"City\": { \"type\": \"string\"},\r\n" +
                         "                \"State\": { \"type\": \"string\"},\r\n" +
                         "                \"Country\": { \"type\": \"string\"},\r\n" +
                         "                \"PostalCode\": { \"type\": \"string\"},\r\n" +
                         "                \"Phone\": { \"type\": \"string\"},\r\n" +
                         "                \"Fax\": { \"type\": \"string\"},\r\n" +
                         "                \"Email\": { \"type\": \"string\"}\r\n" +
                         "              }\r\n" +
                         "            },\r\n" +
                         "          \"FirstName\": { \"type\": \"string\"},\r\n" +
                         "          \"LastName\": { \"type\": \"string\"},\r\n" +
                         "          \"Address\": { \"type\": \"string\"},\r\n" +
                         "          \"City\": { \"type\": \"string\"},\r\n" +
                         "          \"State\": { \"type\": \"string\"},\r\n" +
                         "          \"Country\": { \"type\": \"string\"},\r\n" +
                         "          \"PostalCode\": { \"type\": \"string\"},\r\n" +
                         "          \"Phone\": { \"type\": \"string\"},\r\n" +
                         "          \"Fax\": { \"type\": \"string\"},\r\n" +
                         "          \"Email\": { \"type\": \"string\"}\r\n" +
                         "        }\r\n" +
                         "      }";
            
            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();

            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());
            Assert.AreEqual(1, objects.Count);
            Assert.AreEqual("Employee", objects.First().Value.Name);
            Assert.AreEqual("Employee", objects.First().Value.Properties[4].Type);
            Assert.AreEqual("SupportRepresentant", obj.Properties[2].Name);
            Assert.AreEqual("Employee", obj.Properties[2].Type);
        }

        [Test]
        public void should_parse_array_in_type_object()
        {
            const string schema = @"{
        '$schema': 'http://json-schema.org/draft-03/schema#',
        'type': 'object',
        'properties': {
            'prop1': { 
                'type': ['object', 'null'],
                'properties':
                 {
                    'readonly': { 'type' : 'boolean' }
                 }
            }
        },
    }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(1, obj.Properties.Count);
            Assert.AreEqual("Prop1", obj.Properties.First().Type);
            Assert.AreEqual(1, objects.Count);
            Assert.AreEqual(1, objects.First().Value.Properties.Count);
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_array_in_type_integer()
        {
            const string schema = @"{
        '$schema': 'http://json-schema.org/draft-03/schema#',
        'type': 'object',
        'properties': {
            'prop1': { 
                'type': ['integer', 'null']
            }
        },
    }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(1, obj.Properties.Count);
            Assert.AreEqual("int?", obj.Properties.First().Type);
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_array_in_type_number()
        {
            const string schema = @"{
        '$schema': 'http://json-schema.org/draft-03/schema#',
        'type': 'object',
        'properties': {
            'prop1': { 
                'type': ['number', 'null']
            }
        },
    }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(1, obj.Properties.Count);
            Assert.AreEqual("decimal?", obj.Properties.First().Type);
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_array_in_type_boolean()
        {
            const string schema = @"{
        '$schema': 'http://json-schema.org/draft-03/schema#',
        'type': 'object',
        'properties': {
            'prop1': { 
                'type': ['null', 'boolean']
            }
        },
    }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(1, obj.Properties.Count);
            Assert.AreEqual("bool?", obj.Properties.First().Type); 
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_array_in_type_string()
        {
            const string schema = @"{
        '$schema': 'http://json-schema.org/draft-03/schema#',
        'type': 'object',
        'properties': {
            'prop1': { 
                'type': ['string', 'null']
            }
        },
    }";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(1, obj.Properties.Count);
            Assert.AreEqual("string", obj.Properties.First().Type); 
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_booleans()
        {
            const string schema = 
                "    {  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                "         \"type\": \"object\",\r\n" +
                "         \"description\": \"A single support status\",\r\n" +
                "         \"properties\": {\r\n" +
                "           \"id\":  { \"type\": \"string\", \"required\": true },\r\n" +
                "           \"name\": { \"type\": \"string\", \"required\": true },\r\n" +
                "           \"exampleBoolProp\": { \"type\": \"boolean\", \"required\": true }\r\n" +
                "         }\r\n" +
                "    }\r\n";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(3, obj.Properties.Count);
            Assert.AreEqual(1, obj.Properties.Count(p => p.Type == "bool"));
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_integers()
        {
            const string schema =
                "    {  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                "         \"type\": \"object\",\r\n" +
                "         \"description\": \"A single support status\",\r\n" +
                "         \"properties\": {\r\n" +
                "           \"id\":  { \"type\": \"string\", \"required\": true },\r\n" +
                "           \"name\": { \"type\": \"string\", \"required\": true },\r\n" +
                "           \"exampleIntProp\": { \"type\": \"integer\", \"required\": true }\r\n" +
                "         }\r\n" +
                "    }\r\n";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(3, obj.Properties.Count);
            Assert.AreEqual(1, obj.Properties.Count(p => p.Type == "int"));
            Assert.AreEqual(0, warnings.Count);
        }

        [Test]
        public void should_parse_bodyless_arrays()
        {
            const string schema =
                "    {  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                "         \"type\": \"object\",\r\n" +
                "         \"description\": \"A single support status\",\r\n" +
                "         \"properties\": {\r\n" +
                "           \"id\":  { \"type\": \"string\", \"required\": true },\r\n" +
                "           \"names\": { \"type\": \"array\", \"required\": true },\r\n" +
                "           \"titles\": { \"type\": \"array\", \r\n" +
                "                 \"items\": \r\n" +
                "                   {\r\n" +
                "                      \"type\": \"object\",\r\n" +
                "                       \"properties\": \r\n" +
                "                       {\r\n" +
                "                          \"id\": { \"type\": \"string\", \"required\": true },\r\n" +
                "                          \"at\": { \"type\": \"string\", \"required\": true }\r\n" +
                "                       }\r\n" +
                "                   }\r\n" +
                "               }\r\n" +
                "         }\r\n" +
                "    }\r\n";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(3, obj.Properties.Count);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("string"), obj.Properties.First(p => p.Name == "Names").Type);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Titles"), obj.Properties.First(p => p.Name == "Titles").Type);
        }

        [Test]
        public void should_parse_bodyless_objects()
        {
            const string schema =
                "    {  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                "         \"type\": \"object\",\r\n" +
                "         \"description\": \"A single support status\",\r\n" +
                "         \"properties\": {\r\n" +
                "           \"id\":  { \"type\": \"string\", \"required\": true },\r\n" +
                "           \"name\": { \"type\": \"object\", \"required\": true },\r\n" +
                "           \"title\": { \"type\": \"object\", \r\n" +
                "                 \"properties\": \r\n" +
                "                   {\r\n" +
                "                      \"id\": { \"type\": \"string\", \"required\": true },\r\n" +
                "                      \"at\": { \"type\": \"string\", \"required\": true }\r\n" +
                "                   }\r\n" +
                "            }\r\n" +
                "         }\r\n" +
                "    }\r\n";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual(3, obj.Properties.Count);
            Assert.AreEqual("Title", obj.Properties.First(p => p.Name == "Title").Type);
            Assert.AreEqual("object", obj.Properties.First(p => p.Name == "Name").Type);
        }

        [Test]
        public void should_parse_properties_as_nullable_when_not_required()
        {
            const string schema = "{\r\n" +
                                  "      \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                                  "      \"type\": \"object\",\r\n" +
                                  "      \"properties\": \r\n" +
                                  "      {\r\n" +
                                  "        \"id\": { \"type\": \"integer\", \"required\": true },\r\n" +
                                  "        \"price\": { \"type\": \"number\", \"required\": true },\r\n" +
                                  "        \"description\": { \"type\": \"string\", \"required\": true },\r\n" +
                                  "        \"optionalPrice\": { \"type\": \"number\" },\r\n" +
                                  "        \"orderItemId\": { \"type\": \"integer\" },\r\n" +
                                  "        \"comment\": { \"type\": \"string\" },\r\n" +
                                  "        \"status\": { \"type\": \"boolean\", }\r\n" +
                                  "      }\r\n" +
                                  "    }\r\n";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());
            
            Assert.AreEqual("int", obj.Properties.First(p => p.Name == "Id").Type);
            Assert.AreEqual("decimal", obj.Properties.First(p => p.Name == "Price").Type);
            Assert.AreEqual("decimal?", obj.Properties.First(p => p.Name == "OptionalPrice").Type);
            Assert.AreEqual("int?", obj.Properties.First(p => p.Name == "OrderItemId").Type);
            Assert.AreEqual("bool?", obj.Properties.First(p => p.Name == "Status").Type);
            Assert.AreEqual("string", obj.Properties.First(p => p.Name == "Description").Type);
            Assert.AreEqual("string", obj.Properties.First(p => p.Name == "Comment").Type);
        }

        [Test]
        public void should_parse_properties_as_nullable_when_not_required_v4()
        {
            const string schema = "{\r\n" +
                                  "      \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
                                  "      \"type\": \"object\",\r\n" +
                                  "      \"properties\": \r\n" +
                                  "      {\r\n" +
                                  "        \"id\": { \"type\": \"integer\" },\r\n" +
                                  "        \"price\": { \"type\": \"number\" },\r\n" +
                                  "        \"optionalPrice\": { \"type\": \"number\" },\r\n" +
                                  "        \"orderItemId\": { \"type\": \"integer\" },\r\n" +
                                  "        \"description\": { \"type\": \"string\" },\r\n" +
                                  "        \"comment\": { \"type\": \"string\" },\r\n" +
                                  "        \"status\": { \"type\": \"boolean\", }\r\n" +
                                  "      },\r\n" +
                                  "    \"required\": [\"id\", \"price\", \"description\"]\r\n" +
                                  "    }\r\n";

            var parser = new JsonSchemaParser();
            var warnings = new Dictionary<string, string>();
            var objects = new Dictionary<string, ApiObject>();
            var enums = new Dictionary<string, ApiEnum>();
            var obj = parser.Parse("name", schema, objects, warnings, enums, new Dictionary<string, ApiObject>(), new Dictionary<string, ApiObject>());

            Assert.AreEqual("int", obj.Properties.First(p => p.Name == "Id").Type);
            Assert.AreEqual("decimal", obj.Properties.First(p => p.Name == "Price").Type);
            Assert.AreEqual("decimal?", obj.Properties.First(p => p.Name == "OptionalPrice").Type);
            Assert.AreEqual("int?", obj.Properties.First(p => p.Name == "OrderItemId").Type);
            Assert.AreEqual("bool?", obj.Properties.First(p => p.Name == "Status").Type);
            Assert.AreEqual("string", obj.Properties.First(p => p.Name == "Description").Type);
            Assert.AreEqual("string", obj.Properties.First(p => p.Name == "Comment").Type);
        }
    }
}