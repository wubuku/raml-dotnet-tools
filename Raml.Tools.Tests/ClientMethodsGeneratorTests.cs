using System.Collections.Generic;
using NUnit.Framework;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class ClientMethodsGeneratorTests
	{
		[Test]
		public void Should_Generate_One_Method_Per_Verb()
		{
			var methods = new List<Method>
			              {
				              new Method
				              {
					              Verb = "get"
				              },
							  new Method
				              {
					              Verb = "post"
				              }
			              };

			var resource = new Resource
			{
				RelativeUri = "/abc{token}{code}",
				Methods = methods,
			};

			var schemaResponseObjects = new Dictionary<string, ApiObject>();
			var schemaRequestObjects = new Dictionary<string, ApiObject>();
			var ramlDocument = new RamlDocument();
			var uriParameterObjects = new Dictionary<string, ApiObject>();
			var queryObjects = new Dictionary<string, ApiObject>();
			var headerObjects = new Dictionary<string, ApiObject>();
			var responseHeadersObjects = new Dictionary<string, ApiObject>();
		    var linkedKeyWithObjectNames = new Dictionary<string, string>();
			var classObject = new ClassObject();

            var generator = new ClientMethodsGenerator(ramlDocument, schemaResponseObjects,
                uriParameterObjects, queryObjects, headerObjects, responseHeadersObjects, schemaRequestObjects, linkedKeyWithObjectNames);

			var generatorMethods = generator.GetMethods(resource, "/", classObject, "Test");

			Assert.AreEqual(2, generatorMethods.Count);
		}
	}
}