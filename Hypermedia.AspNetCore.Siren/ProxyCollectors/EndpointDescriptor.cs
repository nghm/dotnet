using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Util;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal class EndpointDescriptor
    {
        private readonly ActionDescriptor actionDescriptor;
        private readonly object[] arguments;
        private readonly string host;
        private readonly string protocol;

        public EndpointDescriptor(ActionDescriptor actionDescriptor, object[] arguments, string host, string protocol)
        {
            this.actionDescriptor = actionDescriptor;
            this.arguments = arguments;
            this.host = host;
            this.protocol = protocol;
        }

        public string Method { get => actionDescriptor.GetHttpMethod().ToUpper(); }
        public object Body { get => actionDescriptor.PickBodyArgument(arguments); }
        public object Query { get => actionDescriptor.BuildQueryObject(arguments); }
        public object Route { get => actionDescriptor.BuildRouteObject(arguments); }
        public string Href { get => ComputeHref(); }
        public IEnumerable<IField> Fields { get => ComputeFields(); }

        private IEnumerable<IField> ComputeFields()
        {
            return Body.AsPropertyEnumerable(true)
                .Select(kvp => 
                    MakeFieldField(
                        kvp.Key,
                        kvp.Value,
                        actionDescriptor
                    ));
        }

        internal IField MakeFieldField(string key, object value, ActionDescriptor descriptor)
        {
            var bodyParameterInfo = descriptor
                .Parameters
                .Single(parameter => parameter.BindingInfo.BindingSource.Id == "Body");

            if (bodyParameterInfo.ParameterType.GetProperty(key) == null)
            {
                return null;
            }

            var fieldGenerationContext = new FieldGenerationContext
            {
                Name = key,
                Value = value,
                ParamterInfo = bodyParameterInfo
            };

            return new Field(key, value, GetSupportedFieldMetadata(fieldGenerationContext));
        }

        private IEnumerable<IFieldMetadata> GetSupportedFieldMetadata(FieldGenerationContext fieldGenerationContext)
        {
            yield return new TypeMetadata(fieldGenerationContext);
        }

        string ComputeHref()
        {
            var parameters = actionDescriptor.Parameters;
            var queryParameters = new Dictionary<string, object>();
            var routeParameters = new Dictionary<string, object>();

            for (var index = 0; index < parameters.Count; index++)
            {
                var value = arguments[index];
                var info = parameters[index] as ControllerParameterDescriptor;

                if (value == null || info.ParameterInfo.DefaultValue.Equals(value))
                {
                    continue;
                }

                switch (info.BindingInfo.BindingSource.Id)
                {
                    case "Query": queryParameters[info.Name] = value; break;
                    case "Path": routeParameters[info.Name] = value; break;
                }
            }

            var template = actionDescriptor.AttributeRouteInfo.Template.ToLower();

            var href = $"{protocol}://{host}/{template}";

            if (routeParameters != null)
            {
                href = href.InterpolateRouteParameters(routeParameters);
            }

            if (queryParameters != null)
            {
                href = href.InterpolateQueryParameters(queryParameters);
            }

            return href;
        }
    }
}