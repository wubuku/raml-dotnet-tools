using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace RAML.WebApiExplorer
{
	public class SchemaBuilder
	{
        private IDictionary<string, Type> definitions = new Dictionary<string, Type>();
		public string Get(Type type)
		{
		    string schema;

			if (type.IsGenericType && IsGenericWebResult(type))
				type = type.GetGenericArguments()[0];

		    if (IsArrayOrEnumerable(type))
		    {
		        var elementType = type.GetElementType() ?? type.GetGenericArguments()[0];
		        if (elementType.GetProperties().Count(p => p.CanWrite && p.SetMethod.IsPublic) == 0)
		            return null;

		        schema = GetMainArraySchema(elementType);
		    }
		    else
		    {
		        if (type.GetProperties().Count(p => p.CanWrite && p.SetMethod.IsPublic) == 0)
		            return null;

		        schema = GetMainObjectSchema(type);
		    }

		    if (definitions.Any())
		    {
		        schema = AddTrailingComma(schema);
		        schema += GetDefinitions();
		    }

		    return schema;
		}

	    private static string AddTrailingComma(string schema)
	    {
            return schema.Substring(0, schema.Length) + "," + Environment.NewLine;
	    }

	    private string GetDefinitions()
	    {
	        var schema = "\"definitions\": {" + Environment.NewLine;
	        foreach (var definition in definitions)
	        {

	            schema += "  \"" + definition.Key + "\": {" + Environment.NewLine;
	            schema += GetProperties(definition.Value, 4);
                schema += "  }," + Environment.NewLine;
	        }
	        schema = RemoveTrailingComma(schema);
            schema += "}" + Environment.NewLine;
	        return schema;
	    }

	    private static string RemoveTrailingComma(string schema)
	    {
	        schema = schema.Substring(0, schema.Length - 2) + Environment.NewLine;
	        return schema;
	    }

	    private string GetMainObjectSchema(Type type)
	    {
	        var objectSchema = "{ \r\n" +
	                           "  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
	                           "  \"type\": \"object\",\r\n" +
	                           "  \"properties\": {\r\n";

	        objectSchema += GetProperties(type, 4);

	        objectSchema += "  }\r\n";
	        objectSchema += "}\r\n";
	        return objectSchema;
	    }

	    private string GetMainArraySchema(Type elementType)
	    {
	        var arraySchema = "{\r\n" +
	                          "  \"$schema\": \"http://json-schema.org/draft-03/schema\",\r\n" +
	                          "  \"type\": \"array\",\r\n" +
	                          "  \"items\": \r\n" +
	                          "  {\r\n" +
	                          "    \"type\": \"object\",\r\n" +
	                          "    \"properties\": \r\n" +
	                          "    {\r\n";

	        arraySchema += GetProperties(elementType, 6);

	        arraySchema += "    }\r\n";
	        arraySchema += "  }\r\n";
	        arraySchema += "}\r\n";
	        return arraySchema;
	    }

	    private static bool IsGenericWebResult(Type type)
		{
			return (type.GetGenericTypeDefinition() == typeof (System.Web.Http.Results.OkNegotiatedContentResult<>)
					|| type.GetGenericTypeDefinition() == typeof(System.Web.Http.Results.NegotiatedContentResult<>)
					|| type.GetGenericTypeDefinition() == typeof(System.Web.Http.Results.CreatedAtRouteNegotiatedContentResult<>)
					|| type.GetGenericTypeDefinition() == typeof(System.Web.Http.Results.CreatedNegotiatedContentResult<>)
					|| type.GetGenericTypeDefinition() == typeof(System.Web.Http.Results.FormattedContentResult<>)
					|| type.GetGenericTypeDefinition() == typeof(System.Web.Http.Results.JsonResult<>));
		}

		private string GetRecursively(Type type, int pad)
		{
			if (type.IsGenericType && IsGenericWebResult(type))
				type = type.GetGenericArguments()[0];

			if (IsArrayOrEnumerable(type))
			{
				var elementType = type.GetElementType() ?? type.GetGenericArguments()[0];

			    if (SchemaTypeMapper.Map(elementType) != null)
			        return GetNestedArraySchemaPrimitiveType(pad, elementType);

				if (elementType.GetProperties().Count(p => p.CanWrite && p.SetMethod.IsPublic) == 0)
					return string.Empty;

			    return GetNestedArraySchema(pad, elementType);
			}
		    
            if (type.GetProperties().Count(p => p.CanWrite && p.SetMethod.IsPublic) == 0)
		        return string.Empty;

		    return GetNestedObjectSchema(type, pad);
		}

	    private string GetNestedObjectSchema(Type type, int pad)
	    {
	        var objectSchema = "{ \r\n".Indent(pad) +
	                           "  \"type\": \"object\",\r\n".Indent(pad) +
	                           "  \"properties\": {\r\n".Indent(pad);

	        objectSchema += GetProperties(type, pad + 2);

	        objectSchema += "  }\r\n".Indent(pad);
	        objectSchema += "}\r\n".Indent(pad);
	        return objectSchema;
	    }

	    private string GetNestedArraySchema(int pad, Type elementType)
	    {
	        var arraySchema = "{\r\n".Indent(pad) +
	                          "  \"type\": \"array\",\r\n".Indent(pad) +
	                          "  \"items\": \r\n".Indent(pad) +
	                          "  {\r\n".Indent(pad) +
	                          "    \"type\": \"object\",\r\n".Indent(pad) +
	                          "    \"properties\": \r\n".Indent(pad) +
	                          "    {\r\n".Indent(pad);


	        arraySchema += GetProperties(elementType, pad + 4);

	        arraySchema += "    }\r\n".Indent(pad);
	        arraySchema += "  }\r\n".Indent(pad);
	        arraySchema += "}\r\n".Indent(pad);
	        return arraySchema;
	    }

        private string GetNestedArraySchemaPrimitiveType(int pad, Type elementType)
        {
            var arraySchema = "{\r\n".Indent(pad) +
                              "  \"type\": \"array\",\r\n".Indent(pad) +
                              "  \"items\": \r\n".Indent(pad) +
                              "  {\r\n".Indent(pad) +
                              ("    \"type\": \"" + SchemaTypeMapper.Map(elementType) + "\",\r\n").Indent(pad) +
                              "  }\r\n".Indent(pad) +
                              "}\r\n".Indent(pad);
            return arraySchema;
        }

	    private string GetProperties(Type elementType, int pad)
		{
			var schema = string.Empty;
			var props = elementType.GetProperties().Where(p => p.CanWrite && p.SetMethod.IsPublic).ToArray();
			foreach (var prop in props)
			{
				schema = GetProperty(pad, prop, schema, props);
			}
			return schema;
		}

	    private string GetProperty(int pad, PropertyInfo prop, string schema, PropertyInfo[] props)
	    {
	        schema = SchemaTypeMapper.Map(prop.PropertyType) != null ? 
                HandlePrimitiveTypeProperty(pad, prop, props, schema) : 
                HandleNestedTypeProperty(pad, prop, schema, props);
	        return schema;
	    }

	    private static string HandlePrimitiveTypeProperty(int pad, PropertyInfo prop, PropertyInfo[] props, string schema)
	    {
	        if (prop == props.Last())
	            schema += BuildLastProperty(prop, pad);
	        else
	            schema += BuildProperty(prop, pad);
	        return schema;
	    }

	    private string HandleNestedTypeProperty(int pad, PropertyInfo prop, string schema, IEnumerable<PropertyInfo> props)
	    {
	        var nestedType = GetRecursively(prop.PropertyType, pad + 2);
	        if (!string.IsNullOrWhiteSpace(nestedType))
	        {
	            schema += ("\"" + prop.Name + "\":").Indent(pad) + Environment.NewLine;
	            schema += nestedType;
	        }
	        else
	        {
	            // is it one of ?
	            var subclasses =prop.PropertyType.Assembly.GetTypes()
                    .Where(type => type.IsSubclassOf(prop.PropertyType))
                    .ToArray();
	            if (subclasses.Any())
	                schema += GetOneOfProperty(prop, subclasses, pad);
	        }

	        if (prop != props.Last())
	            schema = schema.Substring(0, schema.Length - "\r\n".Length) + ",\r\n";

	        return schema;
	    }

	    private string GetOneOfProperty(PropertyInfo prop, IEnumerable<Type> subclasses, int pad)
	    {
	        var oneOf = ("\"" + prop.Name + "\": {").Indent(pad) + Environment.NewLine;
	        oneOf += "\"type\": \"object\",".Indent(pad + 2) + Environment.NewLine;
	        oneOf += "\"oneOf\": [".Indent(pad + 2) + Environment.NewLine;
	        foreach (var subclass in subclasses)
	        {
                oneOf += ("{ \"$ref\": \"#/definitions/" + subclass.Name + "\" },").Indent(pad + 4) + Environment.NewLine;
                definitions.Add(subclass.Name, subclass);
	        }
	        oneOf = oneOf.Substring(0, oneOf.Length - Environment.NewLine.Length - 1) + Environment.NewLine;
	        oneOf += "]".Indent(pad + 2) + Environment.NewLine;
	        oneOf += "}".Indent(pad) + Environment.NewLine;
	        return oneOf;
	    }

	    private static string BuildProperty(PropertyInfo prop, int pad)
		{
			var res = "\"" + prop.Name.ToLowerInvariant() + "\": { \"type\": \"" + SchemaTypeMapper.Map(prop.PropertyType) + "\"";
			
			if (IsNullable(prop.PropertyType))
				res += ", \"required\": \"false\"";

			res += "},\r\n";

			res = res.Indent(pad);
			return res;
		}

		private static string BuildLastProperty(PropertyInfo prop, int pad)
		{
			var res = "\"" + prop.Name.ToLowerInvariant() + "\": { \"type\": \"" + SchemaTypeMapper.Map(prop.PropertyType) + "\"";

			if (IsNullable(prop.PropertyType))
				res += ", \"required\": \"false\"";

			res += "}\r\n";

			res = res.Indent(pad);
			return res;
		}

		public static bool IsNullable(Type t)
		{
			return Nullable.GetUnderlyingType(t) != null;
		}

		private static bool IsArrayOrEnumerable(Type type)
		{
			return type.IsArray || (type.IsGenericType &&
			                        (type.GetGenericTypeDefinition() == typeof (IEnumerable<>)
			                         || type.GetGenericTypeDefinition() == typeof (ICollection<>)
			                         || type.GetGenericTypeDefinition() == typeof (IList<>)));
		}

	}
}