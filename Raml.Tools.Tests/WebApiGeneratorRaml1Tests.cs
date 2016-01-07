using System.IO;
using NUnit.Framework;
using Raml.Parser;
using Raml.Tools.ClientGenerator;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Raml.Tools.WebApiGenerator;

namespace Raml.Tools.Tests
{
    [TestFixture]
    public class WebApiGeneratorRaml1Tests
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
        public async Task ShouldDetectArrayTypes_WhenMovies()
        {
            var model = await GetMoviesModel();
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Movie"), model.Controllers.First(o => o.Name == "Movies").Methods.First(m => m.Name == "Get").ReturnType);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Movie"), model.Controllers.First(o => o.Name == "Movies").Methods.First(m => m.Name == "GetAvailable").ReturnType);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Movie"), model.Controllers.First(o => o.Name == "Search").Methods.First(m => m.Name == "Get").ReturnType);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Movie"), model.Controllers.First(o => o.Name == "Movies").Methods.First(m => m.Name == "Post").Parameter.Type);
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
            Assert.IsNotNull(model);
        }

        [Test]
        public async Task ShouldBuild_WhenChinook()
        {
            var model = await BuildModel("files/raml1/chinook-v1.raml");
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Artist"), model.Controllers.First(c => c.Name == "Artists").Methods.First(m => m.Name == "Get").ReturnType);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Album"), model.Controllers.First(c => c.Name == "Albums").Methods.First(m => m.Name == "Get").ReturnType);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Customer"), model.Controllers.First(c => c.Name == "Customers").Methods.First(m => m.Name == "Get").ReturnType);
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Track"), model.Controllers.First(c => c.Name == "Tracks").Methods.First(m => m.Name == "Get").ReturnType);

            Assert.AreEqual("Artist", model.Controllers.First(c => c.Name == "Artists").Methods.First(m => m.Name == "GetById").ReturnType);
            Assert.AreEqual("Album", model.Controllers.First(c => c.Name == "Albums").Methods.First(m => m.Name == "GetById").ReturnType);
            Assert.AreEqual("Customer", model.Controllers.First(c => c.Name == "Customers").Methods.First(m => m.Name == "GetById").ReturnType);
            Assert.AreEqual("Track", model.Controllers.First(c => c.Name == "Tracks").Methods.First(m => m.Name == "GetById").ReturnType);

            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("InvoiceLine"), model.Objects.First(c => c.Name == "Invoice").Properties.First(p => p.Name == "Lines").Type);
        }

        private static async Task<WebApiGeneratorModel> GetAnnotationTargetsModel()
        {
            return await BuildModel("files/raml1/annotations-targets.raml");
        }

        private static async Task<WebApiGeneratorModel> GetAnnotationsModel()
        {
            return await BuildModel("files/raml1/annotations.raml");
        }


        private async Task<WebApiGeneratorModel> GetCustomScalarModel()
        {
            return await BuildModel("files/raml1/customscalar.raml");
        }

        private async Task<WebApiGeneratorModel> GetMoviesModel()
        {
            return await BuildModel("files/raml1/movies-v1.raml");
        }

        private async Task<WebApiGeneratorModel> GetMovieTypeModel()
        {
            return await BuildModel("files/raml1/movietype.raml");
        }

        private async Task<WebApiGeneratorModel> GetParametersModel()
        {
            return await BuildModel("files/raml1/parameters.raml");
        }

        private async Task<WebApiGeneratorModel> GetTypeExpressionsModel()
        {
            return await BuildModel("files/raml1/typeexpressions.raml");
        }


        private static async Task<WebApiGeneratorModel> BuildModel(string ramlFile)
        {
            var fi = new FileInfo(ramlFile);
            var raml = await new RamlParser().LoadAsync(fi.FullName);
            var model = new WebApiGeneratorService(raml, "TargetNamespace").BuildModel();

            return model;
        }
    }
}