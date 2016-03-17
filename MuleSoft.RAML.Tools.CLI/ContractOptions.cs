using CommandLine;

namespace MuleSoft.RAML.Tools.CLI
{
    [Verb("contract", HelpText = "RAML WebApi 2 scaffold (contract)")]
    public class ContractOptions : Options
    {
        [Option('a', "async", Required = false, HelpText = "Use async methods (defaults to not use async)")]

        public bool UseAsyncMethods { get; set; }
    }
}