using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Util
{
    static class ObjectEnumerationExtensions
    {
        public static IEnumerable<KeyValuePair<string, T>> AsPropertyEnumerable<T>(this object obj, bool keepNullValues = false)
        {
            return obj
                .AsPropertyEnumerable(keepNullValues)
                .Where(kvp => kvp.Value is T)
                .Select(kvp => KeyValuePair.Create(kvp.Key, (T) kvp.Value));
        }

        public static IEnumerable<KeyValuePair<string, object>> AsPropertyEnumerable(this object obj, bool keepNullValues = false)
        {
            if (obj is IEnumerable<KeyValuePair<string, object>>)
            {
                foreach (var kvp in obj as IEnumerable<KeyValuePair<string, object>>)
                {
                    yield return kvp;
                }

                yield break;
            }

            if (obj == null)
            {
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
