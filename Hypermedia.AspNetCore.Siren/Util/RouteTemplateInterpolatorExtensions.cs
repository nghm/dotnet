using System;
using System.Text.RegularExpressions;

namespace Hypermedia.AspNetCore.Siren.Util
{
    static class RouteTemplateInterpolatorExtensions
    {
        public static string InterpolateRouteParameters(this string template, object parameters)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }

            var paramertersKvp = parameters.AsPropertyEnumerable();

            foreach (var kvp in paramertersKvp)
            {
                template = Regex.Replace(template, $"\\{{ *{kvp.Key}[:a-zA-Z0-9 ]*\\}}", kvp.Value.ToString());
            }

            return template;
        }
    }
}
