using System;
using System.Collections.Specialized;
using System.Web;

namespace Hypermedia.AspNetCore.Siren.Util
{
    static class QueryParametersInterpolatorExtensions
    {
        public static string InterpolateQueryParameters(this string template, object parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var paramertersKvp = parameters
                .AsPropertyEnumerable();

            NameValueCollection queryParamsNameValueCollection = HttpUtility.ParseQueryString(string.Empty);

            foreach (var kvp in paramertersKvp)
            {
                queryParamsNameValueCollection.Set(kvp.Key, kvp.Value.ToString());
            }

            var queryString = queryParamsNameValueCollection.ToString();

            if (string.IsNullOrEmpty(queryString))
            {
                return template;
            }

            return $"{template}?{queryString}";
        }
    }
}
