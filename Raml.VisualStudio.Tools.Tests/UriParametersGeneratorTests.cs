using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using NUnit.Framework;
using Raml.Parser.Expressions;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class UriParametersGeneratorTests
	{

		[Test]
		public void Should_Build_Uri_Parameters()
		{
			var uriParameters = new Dictionary<string, Parameter>
			                    {
				                    {
					                    "code", new Parameter
					                            {
						                            DisplayName = "code",
						                            Type = "integer"
					                            }
				                    }
			                    };

			var methods = new List<Method>
			              {
				              new Method
				              {
					              Verb = "get"
				              }
			              };

			var resource = new Resource
				                {
					                RelativeUri = "/abc{token}{code}",
					                Methods = methods,
									UriParameters = uriParameters
				                };

			var generator = new UriParametersGenerator();
			var uriParameterObjects = new Dictionary<string, ApiObject>();
			var generatorMethod = new ClientGeneratorMethod { Name = "MethodOne"};

			generator.Generate(resource, "/movies/{bucket}/abc{token}{code}", generatorMethod, uriParameterObjects);

			Assert.AreEqual(1, uriParameterObjects.Count);
			Assert.AreEqual(3, generatorMethod.UriParameters.Count());
			Assert.AreEqual(uriParameterObjects.First().Value.Name, generatorMethod.UriParametersType);
		}

		[Test]
		public void Should_Build_Uri_Parameters_From_Resources()
		{
			var doc = new RamlDocument { Title = "test" };

			var uriParameters = new Dictionary<string, Parameter>
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
					              Verb = "get"
				              }
			              };

			var resources = new Collection<Resource>
			                {
				                new Resource
				                {
					                RelativeUri = "/movies/{token}/abc/{code}/asd/{extra}",
					                Methods = methods,
									UriParameters = uriParameters
				                },
				                new Resource
				                {
					                RelativeUri = "/directors/{a}/abc/{b}{c}/asd/{d}",
					                Methods = methods
				                }
			                };

			doc.Resources = resources;

			var service = new ClientGeneratorService(doc, "test");
			var model = service.BuildModel();
			Assert.AreEqual(2, model.UriParameterObjects.Count);

			var moviesUriParamObj = model.UriParameterObjects.First(o => o.Key.ToLowerInvariant().Contains("movies")).Value;
			var directorsUriParamObj = model.UriParameterObjects.First(o => o.Key.ToLowerInvariant().Contains("directors")).Value;

			Assert.AreEqual(3, moviesUriParamObj.Properties.Count);
			Assert.AreEqual(4, directorsUriParamObj.Properties.Count);
		}

		[Test]
		public void Should_Build_Uri_Parameters_When_Nested_Resources()
		{
			var doc = new RamlDocument { Title = "test" };

			var uriParameters = new Dictionary<string, Parameter>
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
					              Verb = "get"
				              }
			              };

			var resources = new Collection<Resource>
			                {
				                new Resource
				                {
					                RelativeUri = "/movies/{token}{extra}/abc/{code}{extra}/asd",
					                Methods = methods,
					                UriParameters = uriParameters,
					                Resources = new[]
					                            {
						                            new Resource
						                            {
							                            RelativeUri = "/{a}{b}",
							                            Methods = methods
						                            }

					                            }
				                }
			                };

			doc.Resources = resources;

			var service = new ClientGeneratorService(doc, "test");
			var model = service.BuildModel();
			Assert.AreEqual(2, model.UriParameterObjects.Count);

			var moviesUriParamObj = model.UriParameterObjects.First().Value;
			var directorsUriParamObj = model.UriParameterObjects.Last().Value;

			Assert.AreEqual(3, moviesUriParamObj.Properties.Count);
			Assert.AreEqual(5, directorsUriParamObj.Properties.Count);
		}


	}
}