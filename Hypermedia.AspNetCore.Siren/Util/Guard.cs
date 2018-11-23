using System;

namespace Hypermedia.AspNetCore.Siren.Util
{
    internal static class Guard
    {
        internal static void EnsureIsNotNull(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}