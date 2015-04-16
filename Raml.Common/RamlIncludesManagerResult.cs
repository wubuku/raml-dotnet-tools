using System.Collections.Generic;

namespace Raml.Common
{
	public class RamlIncludesManagerResult
	{
		public RamlIncludesManagerResult(string modifiedContents, IEnumerable<string> includedFiles)
		{
			ModifiedContents = modifiedContents;
			IncludedFiles = includedFiles;
		}

		public string ModifiedContents { get; private set; }
		public IEnumerable<string> IncludedFiles { get; private set; }
	}
}