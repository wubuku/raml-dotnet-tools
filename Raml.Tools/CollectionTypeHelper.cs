namespace Raml.Tools
{
    public class CollectionTypeHelper
    {
        public const string CollectionType = "ICollection";

        public static string GetCollectionType(string netType)
        {
            return CollectionType + "<" + netType + ">";
        }

        public static string GetBaseType(string type)
        {
            if (!type.StartsWith(CollectionType)) return type;

            type = type.Replace(CollectionType, string.Empty);
            type = type.Substring(1, type.Length - 2);
            return type;
        }
    }
}