namespace Hypermedia.AspNetCore.Siren.Util
{
    using System.Collections.Generic;
    
    internal static class ObjectEnumerationExtensions
    {
        public static IEnumerable<KeyValuePair<string, object>> AsPropertyEnumerable(this object obj, bool keepNullValues = false)
        {
            switch (obj)
            {
                case IEnumerable<KeyValuePair<string, object>> pairs:
                {
                    foreach (var kvp in pairs)
                    {
                        yield return kvp;
                    }

                    yield break;
                }
                case null:
                    yield break;
            }

            var props = obj
                .GetType()
                .GetProperties();

            foreach (var propInfo in props)
            {
                var value = propInfo.GetValue(obj);

                if (keepNullValues || value != null)
                {
                    yield return KeyValuePair.Create(propInfo.Name, value);
                }
            }
        }
    }
}
