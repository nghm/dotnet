using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Util;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal class EndpointDescriptor
    {
        private readonly ActionDescriptor _actionDescriptor;
        private readonly object[] _arguments;
        private readonly string _host;
        private readonly string _protocol;
        private readonly IAuthorizationFilter[] _auth;

        public EndpointDescriptor(ControllerActionDescriptor actionDescriptor, object[] arguments, string host, string protocol)
        {
            this._actionDescriptor = actionDescriptor;
            this._arguments = arguments;
            this._host = host;
            this._protocol = protocol;

            this._auth = actionDescriptor.FilterDescriptors
                // ReSharper disable once SuspiciousTypeConversion.Global
                .OfType<IAuthorizationFilter>()
                .ToArray();
        }

        public string Method => this._actionDescriptor.GetHttpMethod().ToUpper();
        public object Body => this._actionDescriptor.PickBodyArgument(this._arguments);
        public object Query => this._actionDescriptor.BuildQueryObject(this._arguments);
        public object Route => this._actionDescriptor.BuildRouteObject(this._arguments);
        public string Href => ComputeHref();
        public IEnumerable<IField> Fields { get => ComputeFields(); }

        private IEnumerable<IField> ComputeFields()
        {
            return this.Body.AsPropertyEnumerable(true)
                .Select(kvp => 
                    MakeFieldField(
                        kvp.Key,
                        kvp.Value,
                        this._actionDescriptor
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

        public bool CanAccess()
        {
            _auth.All(auth => auth.)
        }

        private string ComputeHref()
        {
            var parameters = this._actionDescriptor.Parameters;
            var queryParameters = new Dictionary<string, string>();
            var routeParameters = new Dictionary<string, string>();

            for (var index = 0; index < parameters.Count; index++)
            {
                var value = this._arguments[index];
                var info = parameters[index] as ControllerParameterDescriptor;

                if (info != null && (value == null || info.ParameterInfo.DefaultValue.Equals(value)))
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
                }
            }

            var template = this._actionDescriptor.AttributeRouteInfo.Template.ToLower();

            var href = $"{this._protocol}://{this._host}/{template}";

            href = href.InterpolateRouteParameters(routeParameters);
            href = href.InterpolateQueryParameters(queryParameters);
            
            return href;
        }

        public bool IsLink() => AllowsGetMethod() && HasNoBody();

        private bool HasNoBody()
        {
            return this.Body == null;
        }

        private bool AllowsGetMethod()
        {
            return this.Method == "GET";
        }
    }
}