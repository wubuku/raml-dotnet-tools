using Raml.Tools;
using Raml.Tools.ClientGenerator;

namespace Raml.Common
{
	public interface IT4Service
	{
		Result TransformText(string templatePath, ClientGeneratorModel model, string path, string ramlFile, string targetNamespace);
		Result TransformText<T>(string templatePath, string paramName, T param);
	}
}