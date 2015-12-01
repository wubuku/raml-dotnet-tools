using System.IO;
using NUnit.Framework;
using Raml.Parser;
using Raml.Tools.ClientGenerator;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Raml.Tools.Tests
{
    [TestFixture]
    public class ClientGeneratorRaml1Tests
    {

        [Test]
        public async Task ShouldBuild_WhenAnnotationTargets()
        {
            var model = await GetAnnotationTargetsModel();
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuild_WhenAnnotations()
        {
            var model = await GetAnnotationsModel();
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuild_WhenCustomScalar()
        {
            var model = await GetCustomScalarModel();
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuild_WhenMovieType()
        {
            var model = await GetMovieTypeModel();
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuildTypes_WhenMovies()
        {
            var model = await GetMoviesModel();
            Assert.IsTrue(model.Objects.Any(o => o.Name == "Movie"));
            Assert.AreEqual(9, model.Objects.First(o => o.Name == "Movie").Properties.Count);
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuildMapTypes()
        {
            var model = await GetMapModel();
            Assert.IsTrue(model.Objects.Any(o => o.Name == "Person"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfPerson"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfInt"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfObject"));
        }


        [Test]
        public async Task ShouldBuild_WhenParameters()
        {
            var model = await GetParametersModel();
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuild_WhenTypeExpressions()
        {
            var model = await GetTypeExpressionsModel();
            // TODO: check union and array types in generated methods !
            
            Assert.IsNotNull(model);
        }

        private static async Task<ClientGeneratorModel> GetAnnotationTargetsModel()
        {
            return await BuildModel("files/raml1/annotations-targets.raml");
        }

        private static async Task<ClientGeneratorModel> GetAnnotationsModel()
        {
            return await BuildModel("files/raml1/annotations.raml");
        }


        private async Task<ClientGeneratorModel> GetCustomScalarModel()
        {
            return await BuildModel("files/raml1/customscalar.raml");
        }

        private async Task<ClientGeneratorModel> GetMoviesModel()
        {
            return await BuildModel("files/raml1/movies-v1.raml");
        }

        private async Task<ClientGeneratorModel> GetMovieTypeModel()
        {
            return await BuildModel("files/raml1/movietype.raml");
        }

        private async Task<ClientGeneratorModel> GetParametersModel()
        {
            return await BuildModel("files/raml1/parameters.raml");
        }

        private async Task<ClientGeneratorModel> GetTypeExpressionsModel()
        {
            return await BuildModel("files/raml1/typeexpressions.raml");
        }

        private async Task<ClientGeneratorModel> GetMapModel()
        {
            return await BuildModel("files/raml1/maps.raml");
        }

        private static async Task<ClientGeneratorModel> BuildModel(string ramlFile)
        {
            var fi = new FileInfo(ramlFile);
            var raml = await new RamlParser().LoadAsync(fi.FullName);
            var model = new ClientGeneratorService(raml, "test", "TargetNamespace").BuildModel();

            return model;
        }
    }
}