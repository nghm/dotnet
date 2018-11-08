namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Collections.Generic;
    using System.Linq;
    using Util;

    internal class HrefGenerator : IHrefGenerator
    {
        public string ComputeHref(EndpointDescriptor endpointDescriptor)
        {
            var parameters = endpointDescriptor.Parameters;

            var queryParameters = new Dictionary<string, string>();
            var routeParameters = new Dictionary<string, string>();

            for (var index = 0; index < parameters.Count(); index++)
            {
                var value = endpointDescriptor.Arguments[index];
                var info = parameters[index];

                if (info == null)
                {
                    continue;
                }

                if (value == null || info.ParameterInfo.DefaultValue.Equals(value))
                {
                    continue;
                }

                switch (info.BindingInfo.BindingSource.Id)
                {
                    case "Query":
                        queryParameters[info.Name] = value.ToString();
                        break;
                    case "Path":
                        routeParameters[info.Name] = value.ToString();
                        break;
                    default:
                        continue;
                }
            }

            var template = endpointDescriptor.Template;

            var href = $"{endpointDescriptor.Protocol}://{endpointDescriptor.Host}/{template}";

            href = href.InterpolateRouteParameters(routeParameters);
            href = href.InterpolateQueryParameters(queryParameters);
            
            return href;
        }
    }
}