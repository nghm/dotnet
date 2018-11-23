using System;

namespace Hypermedia.AspNetCore.Siren.Util
{
    internal static class ParameterUtils
    {
        internal static void NullCheck(object value, string name)
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}