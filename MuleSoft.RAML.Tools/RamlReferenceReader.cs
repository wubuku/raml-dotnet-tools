using System;
using System.IO;

namespace MuleSoft.RAML.Tools
{
	public static class RamlReferenceReader
	{
		public static string GetRamlNamespace(string referenceFilePath)
		{
			var contents = File.ReadAllText(referenceFilePath);
			var lines = contents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			return lines[2].Replace("namespace:", string.Empty).Trim();
		}

		public static string GetRamlSource(string referenceFilePath)
		{
			var contents = File.ReadAllText(referenceFilePath);
			var lines = contents.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
			var source = lines[1].Replace("source:", string.Empty).Trim();
			return source;
		}
 
	}
}