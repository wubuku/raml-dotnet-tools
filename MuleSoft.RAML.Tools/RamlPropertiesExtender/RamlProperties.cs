using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Raml.Common;

namespace MuleSoft.RAML.Tools.RamlPropertiesExtender
{
    [ComVisible(true)]
    public class RamlProperties
    {
        private readonly string refFilePath;
        private string ns;
        private string source;

        public RamlProperties(string refFilePath)
        {
            this.refFilePath = refFilePath;
        }

        [Category("RAML Metadata")]
        [Description("Namespace to generate code into")]
        public string Namespace
        {
            get
            {
                if(ns == null)
                    ns = RamlReferenceReader.GetRamlNamespace(refFilePath);
                return ns;
            }
            set
            {
                ns = value; 
                Save();
            }
        }

        [Category("RAML Metadata")]
        [Description("RAML source")]
        public string Source
        {
            get
            {
                if (source == null)
                    source = RamlReferenceReader.GetRamlSource(refFilePath);
                return source; 
                
            }
            set
            {
                source = value;
                Save();
            }
        }

        private void Save()
        {
            var contents = RamlPropertiesManager.BuildContent(Namespace, Source);
            var fileInfo = new FileInfo(refFilePath) { IsReadOnly = false };
            File.WriteAllText(refFilePath, contents);
            fileInfo.IsReadOnly = true;
        }
    }
}