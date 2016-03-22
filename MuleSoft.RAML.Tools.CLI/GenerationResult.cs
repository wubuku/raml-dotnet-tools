using System.CodeDom.Compiler;
using System.Text;

namespace MuleSoft.RAML.Tools.CLI
{
    public class GenerationResult
    {
        public string Content { get; set; }
        public Encoding Encoding { get; set; }

        public CompilerErrorCollection Errors { get; set; }
    }
}