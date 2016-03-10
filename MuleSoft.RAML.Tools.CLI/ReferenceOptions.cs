using CommandLine;

namespace MuleSoft.RAML.Tools.CLI
{
    [Verb("reference", HelpText = "Add/Update RAML reference (client proxy).")]
    class ReferenceOptions 
    {
        [Option('s', "source", Required = true, HelpText = "RAML source URL or file.")]
        public string Source { get; set; }

        [Option('a', "add", Required = false, HelpText = "Add a RAML reference", Default = true)]
        public bool Add { get; set; }

        [Option('u', "update", Required = false, HelpText = "Update a RAML reference", Default = false)]
        public bool Update { get; set; }

        [Option('n', "namespace", Required = false, HelpText = "Target namespace")]
        public string Namespace { get; set; }

        [Option('f', "file", Required = false, HelpText = "RAML target file name (defaults to source name)")]
        public string File { get; set; }

        [Option('t', "title", Required = false, HelpText = "RAML title")]
        public string Title { get; set; }


    }
}