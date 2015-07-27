using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Raml.Common
{
    [ComVisible(true)]
    public class RamlProperties
    {
        [Category("RAML Metadata")]
        [Description("Namespace to generate code into")]
        public string Namespace { get; set; }

        [Category("RAML Metadata")]
        [Description("RAML source")]
        public string Source { get; set; }

    }
}