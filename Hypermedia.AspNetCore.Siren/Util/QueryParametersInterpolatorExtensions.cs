namespace Hypermedia.AspNetCore.Siren.Util
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    [Obsolete]
    internal static class QueryParametersInterpolatorExtensions
    {
        public static string InterpolateQueryParameters(this string template, IDictionary<string, string> parameters)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var queryParamsString = HttpUtility.ParseQueryString(string.Empty);

            foreach (var kvp in parameters)
            {
                queryParamsString.Set(kvp.Key, kvp.Value);
            }

            var queryString = queryParamsString.ToString();

            return string.IsNullOrEmpty(queryString) ? template : $"{template}?{queryString}";
        }
    }
}
