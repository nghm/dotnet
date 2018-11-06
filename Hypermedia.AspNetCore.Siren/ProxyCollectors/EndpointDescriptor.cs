namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using Actions.Fields;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using Actions;
    using Util;

    internal class EndpointDescriptor
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IFieldMetadataProviderCollection _metadataProviderCollection;
        private readonly ControllerActionDescriptor _actionDescriptor;
        private readonly object[] _arguments;
        private readonly string _host;
        private readonly string _protocol;
        private readonly AuthorizationPolicy[] _policies;

        public EndpointDescriptor(
            IAuthorizationService authorizationService,
            IFieldMetadataProviderCollection metadataProviderCollection,
            ControllerActionDescriptor actionDescriptor, 
            object[] arguments, 
            string host, 
            string protocol)
        {
            this._authorizationService = authorizationService;
            this._metadataProviderCollection = metadataProviderCollection;
            this._actionDescriptor = actionDescriptor;
            this._arguments = arguments;
            this._host = host;
            this._protocol = protocol;
            this._policies = actionDescriptor
                .FilterDescriptors
                .Select(f => f.Filter)
                .OfType<AuthorizeFilter>()
                .Select(filter => filter.Policy)
                .ToArray();
        }

        public string Method => this._actionDescriptor.GetHttpMethod().ToUpper();
        public object Body => this._actionDescriptor.PickBodyArgument(this._arguments);
        public string Href => ComputeHref();
        public IEnumerable<IField> Fields => ComputeFields();

        private IEnumerable<IField> ComputeFields()
        {
            return this.Body
                .AsPropertyEnumerable(true)
                .Select(kvp => 
                    MakeField(
                        kvp.Key,
                        kvp.Value,
                        this._actionDescriptor
                    ));
        }

        internal IField MakeField(string key, object value, ControllerActionDescriptor descriptor)
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
                ParameterInfo = bodyParameterInfo
            };

            var metadata = this._metadataProviderCollection
                .GetMetadataProviders()
                .SelectMany(metaProvider => metaProvider.GetMetadata(fieldGenerationContext));

            return new Field(key, value, metadata);
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
            var parameters = this._actionDescriptor
                .Parameters
                .OfType<ControllerParameterDescriptor>()
                .ToArray();

            var queryParameters = new Dictionary<string, string>();
            var routeParameters = new Dictionary<string, string>();

            for (var index = 0; index < parameters.Count(); index++)
            {
                var value = this._arguments[index];
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

            var template = this._actionDescriptor.AttributeRouteInfo.Template.ToLower();

            var href = $"{this._protocol}://{this._host}/{template}";

            href = href.InterpolateRouteParameters(routeParameters);
            href = href.InterpolateQueryParameters(queryParameters);
            
            return href;
        }
    }
}