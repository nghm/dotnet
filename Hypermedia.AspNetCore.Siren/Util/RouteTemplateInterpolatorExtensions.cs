using System;
using System.Text.RegularExpressions;

namespace Hypermedia.AspNetCore.Siren.Util
{
    using System.Collections.Generic;

    [Obsolete]
    internal static class RouteTemplateInterpolatorExtensions
    {
        public static string InterpolateRouteParameters(this string template, IDictionary<string, string> parameters)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            foreach (var kvp in parameters)
            {
                template = Regex.Replace(template, $"\\{{ *{kvp.Key}[:a-zA-Z0-9 ]*\\}}", kvp.Value);
            }

            return template;
        }
    }
}
