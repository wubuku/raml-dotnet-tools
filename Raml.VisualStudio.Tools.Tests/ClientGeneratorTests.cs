using System.Linq;
using NUnit.Framework;
using Raml.Parser;
using System.Threading.Tasks;
using Raml.Tools.ClientGenerator;

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
			Assert.AreEqual(46, model.Warnings.Count());
		}

		[Test]
		public async Task Warnings_WhenTwitter()
		{
			var model = await GetTwitterGeneratedModel();
			Assert.AreEqual(45, model.Warnings.Count());
		}

		[Test]
		public async Task Warnings_WhenGithub()
		{
			var model = await GetGitHubGeneratedModel();
			Assert.AreEqual(43, model.Warnings.Count());
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
			Assert.AreEqual(8, model.RequestObjects.Count);
		}

		[Test]
		public async Task ShouldBuildRequestObjects_FromBox()
		{
			var model = await GetBoxGeneratedModel();
			Assert.AreEqual(7, model.RequestObjects.Count);
		}

		[Test]
		public async Task ShouldBuildResponseObjects_FromInstagram()
		{
			var model = await GetInstagramGeneratedModel();
			Assert.AreEqual(8, model.ResponseObjects.Count);
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
			Assert.AreEqual(9, model.Classes.SelectMany(c => c.Properties).Count());
		}

		[Test]
		public async Task ShouldBuildPropertiosOnrOOTClass_FromCongo()
		{
			var model = await GetCongoGeneratedModel();
			Assert.AreEqual(3, model.Root.Properties.Count());
		}

		[Test]
		public async Task ShouldBuildPropertiosOnClasses_FromGitHub()
		{
			var model = await GetGitHubGeneratedModel();
			Assert.AreEqual(68, model.Classes.SelectMany(c => c.Properties).Count());
		}

		[Test]
		public async Task ShouldBuildPropertiosOnClasses_FromTwitter()
		{
			var model = await GetTwitterGeneratedModel();
			Assert.AreEqual(90, model.Classes.SelectMany(c => c.Properties).Count());
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
			Assert.AreEqual(56, model.ResponseObjects.Count);
		}

		[Test]
		public async Task ShouldBuildRequestObjects_FromGitHub()
		{
			var model = await GetGitHubGeneratedModel();
			Assert.AreEqual(34, model.RequestObjects.Count);
		}

		[Test]
		public async Task ShouldAvoidObjectsWithFrameworkNames()
		{
			var model = await GetGitHubGeneratedModel();
			Assert.IsFalse(model.Objects.Any(o => o.Value.Name == "Get"));
		}

		[Test]
		public async Task ShouldBuildResponseObjects_FromGitHub()
		{
			var model = await GetGitHubGeneratedModel();
			Assert.AreEqual(55, model.ResponseObjects.Count);
		}




		[Test]
		public async Task ShouldParseArrays()
		{
			var model = await GetTestGeneratedModel();
			Assert.AreEqual("Sales[]", model.Objects["get-sales"].Properties[1].Type);
		}

		[Test]
		public async Task ShouldParseCommonObjects()
		{
			var model = await GetTestGeneratedModel();
			Assert.AreEqual("Exchange", model.Objects["exchange"].Name);
			Assert.AreEqual(3, model.Objects["exchange"].Properties.Count);
		}

		[Test]
		public async Task ShouldParse_Congo()
		{
			var model = await GetCongoGeneratedModel();
			Assert.AreEqual(9, model.Classes.SelectMany(c => c.Methods).Count());
			Assert.AreEqual(25, model.Objects.Count());
		}

		[Test]
		public async Task ShouldBuildUriParametersObjects_FromTwitter()
		{
			var model = await GetTwitterGeneratedModel();
			Assert.AreEqual(95, model.UriParameterObjects.Count());
		}


		private static async Task<ClientGeneratorModel> GetTestGeneratedModel()
		{
            var raml = await new RamlParser().LoadAsync("test.raml");
			var model = new ClientGeneratorService(raml, "test").BuildModel();
			
            return model;
		}

		private static async Task<ClientGeneratorModel> GetBoxGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("box.raml");
			var model = new ClientGeneratorService(raml, "test").BuildModel();
			
            return model;
		}


		private async Task<ClientGeneratorModel> GetLargeGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("large.raml");

			var model = new ClientGeneratorService(raml, "test").BuildModel();
			return model;
		}

		private async Task<ClientGeneratorModel> GetRegressionGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("regression.raml");

			var model = new ClientGeneratorService(raml, "test").BuildModel();
			return model;
		}

		private async Task<ClientGeneratorModel> GetCongoGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("congo-drones-5-f.raml");
			return new ClientGeneratorService(raml, "test").BuildModel();
		}

		private async Task<ClientGeneratorModel> GetInstagramGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("instagram.raml");
			return new ClientGeneratorService(raml, "test").BuildModel();
		}

		private async Task<ClientGeneratorModel> GetTwitterGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("twitter.raml");
			return new ClientGeneratorService(raml, "test").BuildModel();
		}

		private async Task<ClientGeneratorModel> GetGitHubGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("github.raml");
			return new ClientGeneratorService(raml, "test").BuildModel();
		}

		private async Task<ClientGeneratorModel> GetContactsGeneratedModel()
		{
			var parser = new RamlParser();
			var raml = await parser.LoadAsync("contacts.raml");
			return new ClientGeneratorService(raml, "test").BuildModel();
		}

		private static async Task<ClientGeneratorModel> GetMoviesGeneratedModel()
		{
			var raml = await new RamlParser().LoadAsync("movies.raml");
			var model = new ClientGeneratorService(raml, "MoviesApi").BuildModel();

			return model;
		}

	}
}