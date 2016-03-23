using CommandLine;

namespace MuleSoft.RAML.Tools.CLI
{
    [Verb("contract", HelpText = "RAML WebApi 2 scaffold (contract)")]
    public class ContractOptions : Options
    {
        [Option('w', "webapi", Required = false, HelpText = "target WebApi 2 (defaults to ASP.NET 5)")]
        public bool WebApi { get; set; }

        [Option('a', "async", Required = false, HelpText = "Use async methods (defaults to not use async)")]
        public bool UseAsyncMethods { get; set; }
    }
}