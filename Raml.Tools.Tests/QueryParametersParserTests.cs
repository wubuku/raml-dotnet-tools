using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Raml.Parser.Builders;
using Raml.Tools.ClientGenerator;

namespace Raml.Tools.Tests
{
	[TestFixture]
	public class QueryParametersParserTests
	{
		[Test]
		public void should_parse_query_parameters()
		{
			var parameterDynamicRaml = new Dictionary<string, object>
			                           {
				                           {"type", "string"},
				                           {"displayName", "ParameterName"},
				                           {"description", "this is the description"}
			                           };

			var parameters = new Dictionary<string, object> {{"one", parameterDynamicRaml}};

			var dynamicRaml = new Dictionary<string, object> {{"method", "get"}, {"queryParameters", parameters}};

			var parsedParameters = QueryParametersParser.ParseParameters(new MethodBuilder().Build(dynamicRaml));
			Assert.AreEqual(1, parsedParameters.Count);
			Assert.AreEqual("string", parsedParameters.First().Type);
			Assert.AreEqual("One", parsedParameters.First().Name);
		}

		[Test]
		public void should_parse_query_object()
		{
			var parameterDynamicRaml = new Dictionary<string, object>
			                           {
				                           {"type", "string"},
				                           {"displayName", "ParameterName"},
				                           {"description", "this is the description"}
			                           };

			var parameters = new Dictionary<string, object> {{"one", parameterDynamicRaml}};

			var dynamicRaml = new Dictionary<string, object> {{"method", "get"}, {"queryParameters", parameters}};

			var generatedMethod = new ClientGeneratorMethod { Name = "GeneratedMethod"};
			const string objectName = "ObjName";
			var queryObj = QueryParametersParser.GetQueryObject(generatedMethod, new MethodBuilder().Build(dynamicRaml), objectName);
			var parsedParameters = queryObj.Properties;

			Assert.AreEqual(generatedMethod.Name + objectName + "Query", queryObj.Name);
			Assert.AreEqual(1, parsedParameters.Count);
			Assert.AreEqual("string", parsedParameters.First().Type);
			Assert.AreEqual("One", parsedParameters.First().Name);
		}

        [Test]
        public void should_keep_original_names()
        {
            var parameterDynamicRaml = new Dictionary<string, object>
			                           {
				                           {"type", "string"},
				                           {"displayName", "parameter-name"},
				                           {"description", "this is the description"}
			                           };

            var parameters = new Dictionary<string, object> { { "keep-orig-name", parameterDynamicRaml } };

            var dynamicRaml = new Dictionary<string, object> { { "method", "get" }, { "queryParameters", parameters } };

            var parsedParameters = QueryParametersParser.ParseParameters(new MethodBuilder().Build(dynamicRaml));
            Assert.AreEqual("keep-orig-name", parsedParameters.First(p => p.Name == "KeepOrigName").OriginalName);
        }
	}
}