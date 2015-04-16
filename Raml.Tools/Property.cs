using System;
using System.Linq;
using System.Net;

namespace Raml.Tools
{
	[Serializable]
	public class Property
	{
		private readonly string[] reservedWords = { "ref", "out", "in", "base", "long", "int", "short", "bool", "string", "decimal", "float", "double" };
		private string name;

		

		public string Name
		{
			get
			{
				if (reservedWords.Contains(name.ToLowerInvariant()))
					return "Ip" + name.ToLowerInvariant();

				return name;
			}

			set { name = value; }
		}

        public string OriginalName { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public string Example { get; set; }
		public bool Required { get; set; }
		public HttpStatusCode StatusCode { get; set; }

        public string JSONSchema { get; set; }
	}
}