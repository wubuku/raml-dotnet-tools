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
        public async Task ShouldBuildArrayTypes()
        {
            var model = await GetArraysModel();
            Assert.AreEqual(5, model.Objects.Count());
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ArrayOfObjectItem"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ArrayOfPerson"));
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Person"), model.Objects.First(o => o.Name == "ArrayOfPerson").Type);
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ArrayOfInt"));
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("integer"), model.Objects.First(o => o.Name == "ArrayOfInt").Type);
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ArrayOfObject"));
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("ArrayOfObjectItem"), model.Objects.First(o => o.Name == "ArrayOfObject").Type);
        }

        [Test]
        public async Task ShouldBuildMapTypes()
        {
            var model = await GetMapsModel();
            Assert.AreEqual(5, model.Objects.Count());
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfObjectItem"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfPerson"));
            Assert.AreEqual("IDictionary<string,Person>", model.Objects.First(o => o.Name == "MapOfPerson").Type);
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfInt"));
            Assert.AreEqual("IDictionary<string,integer>", model.Objects.First(o => o.Name == "MapOfInt").Type);
            Assert.IsTrue(model.Objects.Any(o => o.Name == "MapOfObject"));
            Assert.AreEqual("IDictionary<string,MapOfObjectItem>", model.Objects.First(o => o.Name == "MapOfObject").Type);
        }


        [Test]
        public async Task ShouldBuild_WhenParameters()
        {
            var model = await GetParametersModel();
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldHandleTypeExpressions()
        {
            var model = await GetTypeExpressionsModel();
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Movie"), model.Classes.First().Methods.First(m => m.Verb == "Get").OkReturnType);
            Assert.AreEqual("string", model.Classes.First().Methods.First(m => m.Verb == "Post").Parameter.Type);
            Assert.AreEqual("string", model.Classes.First().Methods.First(m => m.Verb == "Put").Parameter.Type);
        }

        [Test]
        public async Task ShouldHandleInlinedTypes()
        {
            var model = await BuildModel("files/raml1/inlinetype.raml");
            Assert.AreEqual(2, model.Objects.Count());
            Assert.AreEqual("UsersGetOKResponseContent", model.Classes.First().Methods.First(m => m.Verb == "Get").OkReturnType);
            Assert.AreEqual("UsersPostRequestContent", model.Classes.First().Methods.First(m => m.Verb == "Post").Parameter.Type);
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

        private async Task<ClientGeneratorModel> GetMapsModel()
        {
            return await BuildModel("files/raml1/maps.raml");
        }

        private async Task<ClientGeneratorModel> GetArraysModel()
        {
            return await BuildModel("files/raml1/arrays.raml");
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