// Guids.cs
// MUST match guids.h

using System;

namespace MuleSoft.RAML.Tools
{
    static class GuidList
    {
        public const string guidMuleSoft_RAML_ToolsPackagePkgString = "49d3aa6e-2e80-4568-92e9-4bcb3eb2b40d";
        public const string guidMuleSoft_RAML_ReferencesNodeString = "9556bec5-0574-49ed-a2f7-9c427687cc44";
		public const string guidMuleSoft_RAML_FileNodeString = "61eff566-9e0e-4197-b9c4-8b0dab685768";
		public const string guidMuleSoft_RAML_CmdImplementContractString = "a1da1a77-77da-4ab8-8180-9b9e37c059a1";
		public const string guidMuleSoft_RAML_ProjectNodeString = "50f8cbf6-aacf-457d-aa89-0bc300a9b7ae";
		public const string guidMuleSoft_RAML_UpdateRAMLContractString = "14c03645-17a8-49ae-b301-6a619f3942d9";

        public static readonly Guid guidMuleSoft_RAML_ReferencesNode = new Guid(guidMuleSoft_RAML_ReferencesNodeString);
		public static readonly Guid guidMuleSoft_RAML_FileNode = new Guid(guidMuleSoft_RAML_FileNodeString);
		public static readonly Guid guidMuleSoft_RAML_CmdImplementContract = new Guid(guidMuleSoft_RAML_CmdImplementContractString);
		public static readonly Guid guidMuleSoft_RAML_ProjectNode = new Guid(guidMuleSoft_RAML_ProjectNodeString);
		public static readonly Guid guidMuleSoft_RAML_CmdUpdateRAMLContract = new Guid(guidMuleSoft_RAML_UpdateRAMLContractString);
    };
}