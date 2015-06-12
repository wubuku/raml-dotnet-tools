using System.ComponentModel;

namespace Raml.Common
{
    [Editor(typeof(RamlPropertiesEditor), typeof(RamlPropertiesEditor))]
    public class RamlProperties
    {
        [Category("Metadata")]
        public string Namespace { get; set; }

        [Category("Metadata")]
        public string Source { get; set; }
    }
}