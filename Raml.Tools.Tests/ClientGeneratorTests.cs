using NUnit.Framework;
using Raml.Parser;
using Raml.Tools.ClientGenerator;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;

namespace Raml.Tools.Tests
{
    [TestFixture]
    public class ClientGeneratorTests
    {

        [Test]
        public async Task ShouldBuildMethodsWithTheFullPath_WhenEmptyResourceParents()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(5, model.Classes.SelectMany(c => c.Methods).Count());
        }

        [Test]
        public async Task ShouldBuildQueryObjects_WhenMovies()
        {
            var model = await GetMoviesGeneratedModel();
            Assert.AreEqual(1, model.QueryObjects.Count());
        }

        [Test]
        public async Task Warnings_WhenBox()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(48, model.Warnings.Count());
        }

        [Test]
        public async Task Warnings_WhenTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(60, model.Warnings.Count());
        }

        [Test]
        public async Task Warnings_WhenGithub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(60, model.Warnings.Count());
        }

        [Test]
        public async Task ShouldHaveNoWarnings_WhenCongo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(0, model.Warnings.Count());
        }

        [Test]
        public async Task ShouldHaveNoWarnings_WhenMovies()
        {
            var model = await GetMoviesGeneratedModel();
            Assert.AreEqual(0, model.Warnings.Count());
        }

        [Test]
        public async Task ShouldHaveNoWarnings_WhenFstab()
        {
            var model = await GetFstabGeneratedModel();

            Assert.AreEqual(0, model.Warnings.Count());
        }

        [Test]
        public async Task ShouldGetConcreteImplementationsForDefinitions_WhenFstab()
        {
            var model = await GetFstabGeneratedModel();

            var baseClass = model.Objects.Where(o => o.Name == "Storage").ToArray();
            var implementations = model.Objects.Where(o => o.BaseClass == "Storage").ToArray();
            
            Assert.AreEqual(1, baseClass.Length);
            Assert.AreEqual(4, implementations.Length);
        }

        [Test]
        public async Task ShouldHaveNoWarnings_WhenContacts()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(0, model.Warnings.Count());
        }

        [Test]
        public async Task ShouldBuildQueryObjectsFromTest()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(1, model.QueryObjects.Count());
            Assert.AreEqual(1, model.Classes.SelectMany(c => c.Methods).Count(m => m.Query != null));
        }

        [Test]
        public async Task ShouldBuildQueryObjectsFromCongo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(2, model.QueryObjects.Count());
            Assert.AreEqual(2, model.QueryObjects.Values.Count(m => m.Properties.Any()));
        }

        [Test]
        public async Task ShouldBuildQueryObjectsFromBox()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(13, model.QueryObjects.Count());
            Assert.AreEqual(13, model.Classes.SelectMany(c => c.Methods).Count(m => m.Query != null));
        }

        [Test]
        public async Task ShouldBuildMethodsFromTest()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(4, model.Classes.SelectMany(c => c.Methods).Count());
        }

        [Test]
        public async Task ShouldBuildMethodsFromBox()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(52, model.Classes.SelectMany(c => c.Methods).Count());
        }

        [Test]
        public async Task ShouldBuildMethodsFromLarge()
        {
            var model = await GetLargeGeneratedModel();
            Assert.AreEqual(52, model.Classes.SelectMany(c => c.Methods).Count());
        }

        [Test]
        public async Task ShouldBuildMethodsFromRegression()
        {
            var model = await GetRegressionGeneratedModel();
            Assert.AreEqual(15, model.Classes.SelectMany(c => c.Methods).Count());
        }


        [Test]
        public async Task ShouldBuildNamespace()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual("RemoteVendingAPI", model.Namespace);
        }

        [Test]
        public async Task ShouldBuildRequestObjects_FromTest()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(1, model.RequestObjects.Count);
        }

        [Test]
        public async Task ShouldBuildRequestObjects_FromBox()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(9, model.RequestObjects.Count);
        }

        [Test]
        public async Task ShouldBuildResponseObjects_FromInstagram()
        {
            var model = await GetInstagramGeneratedModel();
            Assert.AreEqual(11, model.ResponseObjects.Count);
        }

        [Test]
        public async Task ShouldBuildClasses_FromTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(107, model.Classes.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromTest()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(2, model.Classes.Count());
        }


        [Test]
        public async Task ShouldBuildPropertiosOnClasses_FromBox()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(16, model.Classes.SelectMany(c => c.Properties).Count());
        }

        [Test]
        public async Task ShouldBuildPropertiosOnrOOTClass_FromCongo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(2, model.Root.Properties.Count());
        }

        [Test]
        public async Task ShouldBuildPropertiosOnClasses_FromGitHub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(91, model.Classes.SelectMany(c => c.Properties).Count());
        }

        [Test]
        public async Task ShouldBuildPropertiosOnClasses_FromTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(91, model.Classes.SelectMany(c => c.Properties).Count());
        }

        [Test]
        public async Task ShouldBuildPropertiosOnClasses_FromContacts()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(2, model.Classes.SelectMany(c => c.Properties).Count());
        }


        [Test]
        public async Task ShouldBuildClasses_FromContacts()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(4, model.Classes.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromGithub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(108, model.Classes.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromCongo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(3, model.Classes.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromBox()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(26, model.Classes.Count());
        }

        [Test]
        public async Task ShouldExcludeOptionsFromMethods()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(0, model.Classes.SelectMany(c => c.Methods).Count(m => m.Verb == "Options"));
        }

        [Test]
        public async Task ShouldExcludePatchFromMethods()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(0, model.Classes.SelectMany(c => c.Methods).Count(m => m.Verb == "Patch"));
        }

        [Test]
        public async Task ShouldBuildResponseObjects_FromTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(61, model.ResponseObjects.Count);
        }

        [Test]
        public async Task ShouldBuildRequestObjects_FromGitHub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(32, model.RequestObjects.Count);
        }

        [Test]
        public async Task ShouldAvoidObjectsWithFrameworkNames()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.IsFalse(model.Objects.Any(o => o.Name == "Get"));
        }

        [Test]
        public async Task ShouldBuildResponseObjects_FromGitHub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(66, model.ResponseObjects.Count);
        }




        [Test]
        public async Task ShouldParseArrays()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(CollectionTypeHelper.GetCollectionType("Sales"), model.ResponseObjects["/sales-getOKResponseContent"].Properties[1].Type);
        }

        [Test]
        public async Task ShouldParseCommonObjects()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual("Exchange", model.SchemaObjects["exchange"].Name);
            Assert.AreEqual(3, model.SchemaObjects["exchange"].Properties.Count);
        }

        [Test]
        public async Task ShouldParse_Congo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(9, model.Classes.SelectMany(c => c.Methods).Count());
            Assert.AreEqual(13, model.Objects.Count());
        }

        [Test]
        public async Task ShouldBuildUriParametersObjects_FromTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(95, model.UriParameterObjects.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromDars()
        {
            var model = await GetDarsGeneratedModel();
            Assert.AreEqual(2, model.SchemaObjects.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromDarsWithParams()
        {
            var model = await GetDarsWithParamsGeneratedModel();
            Assert.AreEqual(2, model.SchemaObjects.Count());
        }


        [Test]
        public async Task ShouldBuildClasses_FromEpi()
        {
            var model = await GetEpiGeneratedModel();
            Assert.AreEqual(2, model.Classes.Count());
        }

        [Test]
        public async Task ShouldBuildClasses_FromFoo()
        {
            var model = await GetFooGeneratedModel();
            Assert.AreEqual(2, model.Classes.Count());
        }

        [Test]
        public async void Should_Name_Schemas_Using_Keys()
        {
            var model = await GetSchemaTestsGeneratedModel();
            Assert.IsTrue(model.Objects.Any(o => o.Name == "Thing"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "Things"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ThingResult"));
            Assert.IsTrue(model.Objects.Any(o => o.Name == "ThingRequest"));
        }

        [Test]
        public async void Should_Generate_Properties_When_Movies()
        {
            var model = await GetMoviesGeneratedModel();
            Assert.AreEqual(88, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Box()
        {
            var model = await GetBoxGeneratedModel();
            Assert.AreEqual(78, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Congo()
        {
            var model = await GetCongoGeneratedModel();
            Assert.AreEqual(42, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Contacts()
        {
            var model = await GetContactsGeneratedModel();
            Assert.AreEqual(5, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_GitHub()
        {
            var model = await GetGitHubGeneratedModel();
            Assert.AreEqual(856, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Instagram()
        {
            var model = await GetInstagramGeneratedModel();
            Assert.AreEqual(128, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Large()
        {
            var model = await GetLargeGeneratedModel();
            Assert.AreEqual(77, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Regression()
        {
            var model = await GetRegressionGeneratedModel();
            Assert.AreEqual(114, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Test()
        {
            var model = await GetTestGeneratedModel();
            Assert.AreEqual(39, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async void Should_Generate_Properties_When_Twitter()
        {
            var model = await GetTwitterGeneratedModel();
            Assert.AreEqual(673, model.Objects.Sum(c => c.Properties.Count));
        }

        [Test]
        public async Task Should_Generate_Valid_XML_Comments_WhenGithub()
        {
            var model = await GetGitHubGeneratedModel();
            var xmlDoc = new XmlDocument();
            foreach (var method in model.Classes.SelectMany(c => c.Methods))
            {
                var xmlComment = GetXml(method.XmlComment);
                var xmlSimpleComment = GetXml(method.XmlSimpleComment);
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlComment));
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlSimpleComment));
            }
        }

        [Test]
        public async Task Should_Generate_Valid_XML_Comments_WhenTwitter()
        {
            var model = await GetTwitterGeneratedModel();
            var xmlDoc = new XmlDocument();
            foreach (var method in model.Classes.SelectMany(c => c.Methods))
            {
                var xmlComment = GetXml(method.XmlComment);
                var xmlSimpleComment = GetXml(method.XmlSimpleComment);
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlComment));
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlSimpleComment));
            }
        }

        [Test]
        public async Task Should_Generate_Valid_XML_Comments_WhenMovies()
        {
            var model = await GetMoviesGeneratedModel();
            var xmlDoc = new XmlDocument();
            foreach (var method in model.Classes.SelectMany(c => c.Methods))
            {
                var xmlComment = GetXml(method.XmlComment);
                var xmlSimpleComment = GetXml(method.XmlSimpleComment);
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlComment));
                Assert.DoesNotThrow(() => xmlDoc.LoadXml(xmlSimpleComment));
            }
        }

        [Test]
        public async Task ShouldGenerateEnums_WhenFstab()
        {
            var model = await GetFstabGeneratedModel();
            Assert.AreEqual(4, model.Enums.Count());
            Assert.AreEqual(5, model.Objects.Sum(o => o.Properties.Count(p => p.IsEnum)));
        }

        [Test]
        public async Task ShouldGenerateModels_WhenExternalRefs()
        {
            var model = await GetExternalRefsGeneratedModel();
            Assert.AreEqual(5, model.Objects.Count());
        }

        [Test]
        public async Task ShouldGenerateDifferentModels_WhenDifferentPropertiesWithSameName()
        {
            var model = await GetSameNameGeneratedModel();
            Assert.AreEqual(2, model.Objects.Count(o => o.Name.ToLowerInvariant().Contains("director")));
        }

        [Test]
        public async Task ShouldGenerateProperties_Issue17()
        {
            var model = await GetIssue17GeneratedModel();
            Assert.IsTrue(model.SchemaObjects.Where(o => o.Key.EndsWith("Content")).All(o => o.Value.Properties.Count == 4));
        }

        [Test]
        public async Task ShouldNotDuplicateClasses()
        {
            var model = await GetDuplicationGeneratedModel();
            Assert.AreEqual(2, model.Objects.Count(o => o.Name.StartsWith("User")));
            Assert.AreEqual(1, model.Objects.Count(o => o.Name.Equals("User")));
        }



        private static string GetXml(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                return comment;

            return "<root>" + comment.Replace("///", string.Empty).Replace("\\\"", "\"") + "</root>";
        }

        private static async Task<ClientGeneratorModel> GetTestGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/test.raml");
            var model = new ClientGeneratorService(raml, "test").BuildModel();
            
            return model;
        }

        private static async Task<ClientGeneratorModel> GetBoxGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/box.raml");
            var model = new ClientGeneratorService(raml, "test").BuildModel();
            
            return model;
        }


        private async Task<ClientGeneratorModel> GetLargeGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/large.raml");

            var model = new ClientGeneratorService(raml, "test").BuildModel();
            return model;
        }

        private async Task<ClientGeneratorModel> GetRegressionGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/regression.raml");

            var model = new ClientGeneratorService(raml, "test").BuildModel();
            return model;
        }

        private async Task<ClientGeneratorModel> GetCongoGeneratedModel()
        {
            var parser = new RamlParser();
            var raml = await parser.LoadAsync("files/congo-drones-5-f.raml");
            return new ClientGeneratorService(raml, "test").BuildModel();
        }

        private async Task<ClientGeneratorModel> GetInstagramGeneratedModel()
        {
            var parser = new RamlParser();
            var raml = await parser.LoadAsync("files/instagram.raml");
            return new ClientGeneratorService(raml, "test").BuildModel();
        }

        private async Task<ClientGeneratorModel> GetTwitterGeneratedModel()
        {
            var parser = new RamlParser();
            var raml = await parser.LoadAsync("files/twitter.raml");
            return new ClientGeneratorService(raml, "test").BuildModel();
        }

        private async Task<ClientGeneratorModel> GetGitHubGeneratedModel()
        {
            var parser = new RamlParser();
            var raml = await parser.LoadAsync("files/github.raml");
            return new ClientGeneratorService(raml, "test").BuildModel();
        }

        private async Task<ClientGeneratorModel> GetContactsGeneratedModel()
        {
            var parser = new RamlParser();
            var raml = await parser.LoadAsync("files/contacts.raml");
            return new ClientGeneratorService(raml, "test").BuildModel();
        }

        private static async Task<ClientGeneratorModel> GetMoviesGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/movies.raml");
            var model = new ClientGeneratorService(raml, "MoviesApi").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetFstabGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/fstab.raml");
            var model = new ClientGeneratorService(raml, "FstabApi").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetDarsGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/dars.raml");
            var model = new ClientGeneratorService(raml, "DarsApi").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetDarsWithParamsGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/darsparam.raml");
            var model = new ClientGeneratorService(raml, "DarsApi").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetEpiGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/epi.raml");
            var model = new ClientGeneratorService(raml, "DarsApi").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetFooGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/foo.raml");
            var model = new ClientGeneratorService(raml, "FooApi").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetSchemaTestsGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/schematests.raml");
            var model = new ClientGeneratorService(raml, "SchemaTest").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetExternalRefsGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/external-refs.raml");
            var model = new ClientGeneratorService(raml, "ExternalRefs").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetSameNameGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/same-name-dif-obj.raml");
            var model = new ClientGeneratorService(raml, "SameName").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetIssue17GeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/issue17.raml");
            var model = new ClientGeneratorService(raml, "Issue17").BuildModel();

            return model;
        }

        private static async Task<ClientGeneratorModel> GetDuplicationGeneratedModel()
        {
            var raml = await new RamlParser().LoadAsync("files/duplication.raml");
            var model = new ClientGeneratorService(raml, "Duplication").BuildModel();

            return model;
        }
    }
}