using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Collections.Generic;
    using Util;

    [Obsolete]
    internal class HrefFactory : IHrefFactory
    {
        public string MakeHref(IEndpointDescriptor endpointDescriptor)
        {
            ExtractParameters(
                endpointDescriptor.ArgumentsCollection,
                out var queryParameters,
                out var routeParameters
            );

            var template = endpointDescriptor.Template;

            var href = $"{endpointDescriptor.Protocol}://{endpointDescriptor.Host}/{template}";

            href = href.InterpolateRouteParameters(routeParameters);
            href = href.InterpolateQueryParameters(queryParameters);

            return href;
        }

        private static void ExtractParameters(
            ArgumentCollection argumentsCollection,
            out Dictionary<string, string> queryParameters,
            out Dictionary<string, string> routeParameters)
        {
            queryParameters = new Dictionary<string, string>();
            routeParameters = new Dictionary<string, string>();

            foreach (var argument in argumentsCollection)
            {
                if (argument.ValueIsDefaultValue())
                {
                    continue;
                }

                CategorizeParameter(queryParameters, routeParameters, argument);
            }
        }

        private static void CategorizeParameter(
            IDictionary<string, string> queryParameters,
            IDictionary<string, string> routeParameters,
            ActionArgument argument)
        {
            var argumentValue = argument.Value;

            if (argument.BindingSource == BindingSource.Query)
            {
                queryParameters[argument.Name] = argumentValue.ToString();

                return;
            }

            if (argument.BindingSource == BindingSource.Path)
            {
                routeParameters[argument.Name] = argumentValue.ToString();
            }
        }
    }
}