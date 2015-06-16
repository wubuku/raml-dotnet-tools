using EnvDTE;
using Raml.Common;

namespace MuleSoft.RAML.Tools.RamlPropertiesExtender
{
    public class RamlPropertiesExtenderProvider : IExtenderProvider
    {
        public object GetExtender(string ExtenderCATID, string ExtenderName, object ExtendeeObject, IExtenderSite ExtenderSite,
            int Cookie)
        {
            var ramlFilePath = ((VSLangProj.FileProperties) ExtendeeObject).FullPath;
            var refFilePath = InstallerServices.GetRefFilePath(ramlFilePath);
            return new RamlProperties(refFilePath);
        }

        public bool CanExtend(string ExtenderCATID, string ExtenderName, object ExtendeeObject)
        {
            return ((VSLangProj.FileProperties) ExtendeeObject).Extension == ".raml";
        }
    }
}