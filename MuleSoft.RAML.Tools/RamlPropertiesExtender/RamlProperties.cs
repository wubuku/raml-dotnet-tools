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
        private bool? useAsyncMethods;
        private string clientName;

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

        [Category("RAML Metadata")]
        [Description("Use async methods in WebApi controllers")]
        public bool? UseAsyncMethods
        {
            get
            {
                if (useAsyncMethods == null)
                    useAsyncMethods = RamlReferenceReader.GetRamlUseAsyncMethods(refFilePath);
                return useAsyncMethods;

            }
            set
            {
                useAsyncMethods = value;
                Save();
            }
        }

        [Category("RAML Metadata")]
        [Description("Client proxy name")]
        public string ClientName
        {
            get
            {
                if (clientName == null)
                    clientName = RamlReferenceReader.GetClientRootClassName(refFilePath);
                return clientName;

            }
            set
            {
                clientName = value;
                Save();
            }
        }

        private void Save()
        {
            var contents = RamlPropertiesManager.BuildContent(Namespace, Source, UseAsyncMethods, ClientName);
            var fileInfo = new FileInfo(refFilePath) { IsReadOnly = false };
            File.WriteAllText(refFilePath, contents);
            fileInfo.IsReadOnly = true;
        }
    }
}