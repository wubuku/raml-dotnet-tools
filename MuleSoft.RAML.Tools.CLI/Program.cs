using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommandLine;
using Microsoft.VisualStudio.Shell;
using Raml.Common;

namespace MuleSoft.RAML.Tools.CLI
{
    class Program
    {
        static int Main(string[] args)
        {
            Parser.Default.ParseArguments<ReferenceOptions, ContractOptions, string>(args)
                .MapResult(
                    (ReferenceOptions opts) => RunReferenceAndReturnExitCode(opts),
                    (ContractOptions opts) => RunContractAndReturnExitCode(opts),
                    HandleError);

            return 0;
        }

        private static int HandleError(IEnumerable<Error> errors)
        {
            var msg = string.Empty;
            foreach (var error in errors)
            {
                msg += ((NamedError)(error)).NameInfo.LongName;
            }
            throw new ArgumentException(msg);
            return 0;
        }


        private static int RunContractAndReturnExitCode(ContractOptions opts)
        {
            var generationServices = new RamlScaffoldService(new T4Service(ServiceProvider.GlobalProvider),  ServiceProvider.GlobalProvider);
            var ramlFilePath = Path.GetTempPath();
            var ramlTitle = string.IsNullOrWhiteSpace(opts.Title) ? Path.GetFileName(opts.Source) : opts.Title;
            var templatesPath = Assembly.GetAssembly(typeof(Program)).Location;
            var targetFileName = string.IsNullOrWhiteSpace(opts.File) ? Path.GetFileNameWithoutExtension(ramlFilePath) : opts.File;

            if (opts.Add)
            {
                var ramlChooserActionParams = new RamlChooserActionParams(opts.Source, ramlFilePath, ramlTitle, templatesPath,
                    targetFileName, opts.Namespace, false);
                generationServices.AddContract(ramlChooserActionParams);

            }
            return 0;
        }

        private static int RunReferenceAndReturnExitCode(ReferenceOptions opts)
        {
            var generationServices = new RamlReferenceService(ServiceProvider.GlobalProvider);
            var ramlFilePath = Path.GetTempPath();
            var ramlTitle = string.IsNullOrWhiteSpace(opts.Title) ? Path.GetFileName(opts.Source) : opts.Title;
            var templatesPath = Assembly.GetAssembly(typeof(Program)).Location;
            var targetFileName = string.IsNullOrWhiteSpace(opts.File) ? Path.GetFileNameWithoutExtension(ramlFilePath) : opts.File;

            if (opts.Add)
            {
                var ramlChooserActionParams = new RamlChooserActionParams(opts.Source, ramlFilePath, ramlTitle, templatesPath,
                    targetFileName, opts.Namespace, false);
                generationServices.AddRamlReference(ramlChooserActionParams);

            }
            return 0;
        }

    }
}
