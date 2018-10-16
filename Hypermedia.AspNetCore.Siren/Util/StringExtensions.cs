namespace Hypermedia.AspNetCore.Siren.Util
{
    internal static class StringExtensions
    {   
        public static string ToCamelCase(this string target)
        {
            return target.Substring(0, 1).ToLower() + target.Substring(1);
        }
    }
}
