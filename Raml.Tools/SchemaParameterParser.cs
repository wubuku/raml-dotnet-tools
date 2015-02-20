using System.Text.RegularExpressions;
using Raml.Parser.Expressions;
using Raml.Tools.Pluralization;

namespace Raml.Tools
{
	public class SchemaParameterParser
	{
		private readonly IPluralizationService pluralizationService;

		public SchemaParameterParser(IPluralizationService pluralizationService)
		{
			this.pluralizationService = pluralizationService;
		}

		public string Parse(string schema, Resource resource, Method method, string fullUrl)
		{
			var url = resource.RelativeUri;
			url = ReplaceUriParameters(url);

			if (string.IsNullOrWhiteSpace(url) || url.Trim() == "/")
			{
				fullUrl = ReplaceUriParameters(fullUrl);
				url = fullUrl;
			}

			url = url.TrimEnd('/');

			var res = schema.Replace("<<resourcePathName>>", url.Substring(1));
			res = res.Replace("<<resourcePath>>", url);
			if(method != null && method.Verb != null)
				res = res.Replace("<<methodName>>", method.Verb.ToLower());

			var regex = new Regex(@"\<\<(resourcePathName|resourcePath|methodName)\s?\|\s?\!(pluralize|singularize)\>\>", RegexOptions.IgnoreCase);
			var matchCollection = regex.Matches(res);
			foreach (Match match in matchCollection)
			{
				string replacementWord;
				switch (match.Groups[1].Value)
				{
					case "resourcePathName":
						replacementWord = url.Substring(1);
						break;
					case "resourcePath":
						replacementWord = url.Substring(1);
						break;
					case "methodName":
						replacementWord = url.Substring(1);
						break;
					default:
						continue;
				}

				if (match.Groups[2].Value == "singularize")
				{
					res = res.Replace(match.Groups[0].Value, Singularize(replacementWord));
				}
				else if (match.Groups[2].Value == "pluralize")
				{
					res = res.Replace(match.Groups[0].Value, Pluralize(replacementWord));
				}
			}

			return res;
		}

		private static string ReplaceUriParameters(string fullUrl)
		{
			var regexParams = new Regex(@"\{[^\}]+\}");
			fullUrl = regexParams.Replace(fullUrl, string.Empty);
			return fullUrl;
		}

		public string Singularize(string input)
		{
			return pluralizationService.Singularize(input);
		}

		public string Pluralize(string input)
		{
			return pluralizationService.Pluralize(input);
		}
	}
}