using System;

namespace Hypermedia.AspNetCore.Siren.Util
{
    internal static class StringExtensions
    {   
        public static string ToCamelCase(this string target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (target == "")
            {
                return target;
            }

            if (!char.IsLetter(target[0]))
            {
                return target;
            }

            return char.ToLower(target[0]) + target.Substring(1);
        }
    }
}
