using System;
using System.Collections.Generic;

namespace RAML.WebApiExplorer
{
	public class SchemaTypeMapper
	{
		private static readonly IDictionary<Type, string> typeConversion =
			new Dictionary<Type, string>
			{
				{
					typeof (int),
					"integer"
				},
				{
					typeof (string),
					"string"
				},
				{
					typeof (bool),
					"boolean"
				},
				{
					typeof (decimal),
					"number" // float
				},
				{
					typeof (DateTime),
					"string"
				},
				{
					typeof (object),
					"any"
				},
				{
					typeof (int?),
					"[\"integer\",\"null\"]"
				},
				{
					typeof (bool?),
					"[\"boolean\",\"null\"]"
				},
				{
					typeof (decimal?),
					"[\"number\",\"null\"]" // float
				},
				{
					typeof (DateTime?),
					"string"
				}
			};

		public static string Map(Type type)
		{
			return typeConversion.ContainsKey(type) ? typeConversion[type] : null;
		}
	}
}