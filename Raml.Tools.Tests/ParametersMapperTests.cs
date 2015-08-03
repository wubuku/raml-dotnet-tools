using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Raml.Parser.Builders;
using Raml.Parser.Expressions;

namespace Raml.Tools.Tests
{
    [TestFixture]
    public class ParametersMapperTests
    {
        [Test]
        public void should_map_paremeters()
        {
            var dynRaml = new Dictionary<string, object>();
            dynRaml.Add("type", "string");
            dynRaml.Add("displayName", "ParameterName");
            dynRaml.Add("description", "this is the description");
            var parameters = new Dictionary<string, Parameter> {{"one", new ParameterBuilder().Build(dynRaml)}};
            var generatorParameters = ParametersMapper.Map(parameters);
            Assert.AreEqual(parameters.Count, generatorParameters.Count());
            Assert.AreEqual(parameters.First().Value.Type, generatorParameters.First().Type);
            Assert.AreEqual("one", generatorParameters.First().Name);
            Assert.AreEqual(parameters.First().Value.Description, generatorParameters.First().Description);
        }
    }
}