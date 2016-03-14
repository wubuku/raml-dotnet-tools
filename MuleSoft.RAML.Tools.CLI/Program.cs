using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CommandLine;
using EnvDTE80;
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
            //EnvDTE80.DTE2 dte;
            //object obj = null;
            //System.Type t = null;

            //// Get ProgID for DTE 8.0
            //// NOTE: Do not put this call in a try block because it 
            //// will cause Visual Studio to fail to respond.
            //t = System.Type.GetTypeFromProgID("VisualStudio.DTE.8.0", true);

            //// Attempt to create an instance of envDTE. 
            //obj = System.Activator.CreateInstance(t, true);

            //// Cast to DTE2.
            //dte = (EnvDTE80.DTE2)obj;

            System.Type type = Type.GetTypeFromProgID("VisualStudio.DTE.12.0");
            EnvDTE.DTE dte = (EnvDTE.DTE)System.Activator.CreateInstance(type);
            dte.MainWindow.Visible = true;

            // Register the IOleMessageFilter to handle any threading 
            // errors.
            MessageFilter.Register();
            // Display the Visual Studio IDE.
            dte.MainWindow.Activate();

            // =====================================
            // ==Insert your automation code here.==
            // =====================================
            // For example, get a reference to the solution2 object
            // and do what you like with it.
            Solution2 soln = (Solution2)dte.Solution;
            System.Windows.Forms.MessageBox.Show
              ("Solution count: " + soln.Count);
            // =====================================

            // All done, so shut down the IDE...
            dte.Quit();
            // and turn off the IOleMessageFilter.
            MessageFilter.Revoke();

            var generationServices = new RamlReferenceService(ServiceProvider.GlobalProvider, new NullLogger());
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
