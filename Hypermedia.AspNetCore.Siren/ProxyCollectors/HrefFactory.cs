namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Util;

    internal class HrefFactory : IHrefFactory
    {
        public string MakeHref(EndpointDescriptor endpointDescriptor)
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
            IDictionary<ControllerParameterDescriptor, object> argumentsCollection,
            out Dictionary<string, string> queryParameters,
            out Dictionary<string, string> routeParameters)
        {
            queryParameters = new Dictionary<string, string>();
            routeParameters = new Dictionary<string, string>();

            foreach (var argument in argumentsCollection)
            {
                if (ArgumentIsDefaultValue(argument))
                {
                    continue;
                }

                CategorizeParameter(queryParameters, routeParameters, argument);
            }
        }

        private static void CategorizeParameter(
            IDictionary<string, string> queryParameters,
            IDictionary<string, string> routeParameters,
            KeyValuePair<ControllerParameterDescriptor, object> argument)
        {
            var argumentValue = argument.Value;
            var parameterDescriptor = argument.Key;

            switch (parameterDescriptor.BindingInfo.BindingSource.Id)
            {
                case "Query":
                    queryParameters[parameterDescriptor.Name] = argumentValue.ToString();
                    break;
                case "Path":
                    routeParameters[parameterDescriptor.Name] = argumentValue.ToString();
                    break;
                default:
                    return;
            }
        }

        private static bool ArgumentIsDefaultValue(KeyValuePair<ControllerParameterDescriptor, object> argument)
        {
            var parameterInfoDefaultValue = argument.Key.ParameterInfo.DefaultValue;

            return parameterInfoDefaultValue.Equals(argument.Value);
        }
    }
}