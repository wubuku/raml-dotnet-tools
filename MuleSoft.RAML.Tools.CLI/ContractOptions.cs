using CommandLine;

namespace MuleSoft.RAML.Tools.CLI
{
    [Verb("contract", HelpText = "Add/Update RAML contract (WebApi 2 or Mvc 6 scaffold).")]
    public class ContractOptions 
    {
        [Option('s', "source", Required = true, HelpText = "RAML source URL or file.")]
        public string Source { get; set; }

        [Option('a', "add", Required = false, HelpText = "Add a RAML contract", Default = true)]
        public bool Add { get; set; }

        [Option('u', "update", Required = false, HelpText = "Update a RAML contract", Default = false)]
        public bool Update { get; set; }

        [Option('n', "namespace", Required = false, HelpText = "Target namespace")]
        public string Namespace { get; set; }

        [Option('f', "file", Required = false, HelpText = "RAML target file name (defaults to source name)")]
        public string File { get; set; }

        [Option('t', "title", Required = false, HelpText = "RAML title")]
        public string Title { get; set; }

        [Option('s', "scaffold", Required = false, HelpText = "Scaffold RAML contract (default: true)")]
        public bool Scaffold { get; set; }
    }
}