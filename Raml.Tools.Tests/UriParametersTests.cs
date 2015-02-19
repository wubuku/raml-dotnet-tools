using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class UriParametersTests
	{
		[Test]
		public void Should_Build_Uri_Parameter_Objects()
		{
			var doc = new RamlDocument { Title = "test" };

			var uriParams = new Dictionary<string, Parameter>()
			                {
				                {
					                "code", new Parameter
					                        {
						                        DisplayName = "code",
						                        Type = "integer"
					                        }
				                },
				                {
					                "token", new Parameter
					                         {
						                         DisplayName = "token",
						                         Type = "string"
					                         }
				                }
			                };



			var methods = new List<Method>
			              {
				              new Method
				              {
					              Verb = "get",
				              },
							  new Method
							  {
								  Verb = "post"
							  }
			              };

			var resources = new Collection<Resource>
			                {
				                new Resource
				                {
					                RelativeUri = "/movies",
									UriParameters = uriParams,
					                Methods = methods
				                }
			                };

			doc.Resources = resources;

			var service = new ClientGeneratorService(doc, "test");
			var model = service.BuildModel();

			Assert.AreEqual(1, model.UriParameterObjects.Count);
			Assert.AreEqual(2, model.UriParameterObjects.First().Value.Properties.Count);
			Assert.IsTrue(model.Classes.First().Methods.All(m => m.UriParametersType == "MoviesUriParameters"));
			Assert.AreEqual(1, model.Objects.Count);
		}
	}
}