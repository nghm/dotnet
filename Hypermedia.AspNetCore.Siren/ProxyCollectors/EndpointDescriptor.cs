namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using Actions.Fields;
    using Util;

    internal class EndpointDescriptor
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly ControllerActionDescriptor _actionDescriptor;
        private readonly object[] _arguments;
        private readonly string _host;
        private readonly string _protocol;
        private readonly AuthorizationPolicy[] _policies;

        public EndpointDescriptor(
            IAuthorizationService authorizationService,
            ControllerActionDescriptor actionDescriptor, 
            object[] arguments, 
            string host, 
            string protocol)
        {
            this._authorizationService = authorizationService;
            this._actionDescriptor = actionDescriptor;
            this._arguments = arguments;
            this._host = host;
            this._protocol = protocol;
            this._policies =  actionDescriptor
                .FilterDescriptors
                .Select(f => f.Filter)
                .OfType<AuthorizeFilter>()
                .Select(filter => filter.Policy)
                .ToArray();
        }

        public string Method => this._actionDescriptor.GetHttpMethod().ToUpper();
        public object Body => this._actionDescriptor.PickBodyArgument(this._arguments);
        public object Query => this._actionDescriptor.BuildQueryObject(this._arguments);
        public object Route => this._actionDescriptor.BuildRouteObject(this._arguments);
        public string Href => ComputeHref();
        public IEnumerable<IField> Fields => ComputeFields();

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

        public bool CanAccess(ClaimsPrincipal user)
        {
            if (this._policies.Length == 0)
            {
                return true;
            }

            return this._policies
               .Any(authorizationPolicy => IsAuthorized(user, authorizationPolicy));
        }

        private bool IsAuthorized(ClaimsPrincipal user, AuthorizationPolicy authorizationPolicy)
        {
            return this._authorizationService
                .AuthorizeAsync(user, authorizationPolicy)
                .Result
                .Succeeded;
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