using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Linq;
    using Util;

    internal class EndpointDescriptor : IEndpointDescriptor
    {
        public EndpointDescriptor(
            ActionDescriptor actionDescriptor,
            object[] arguments,
            string host,
            string protocol)
        {
            this.Policies = ComputePolicies(actionDescriptor);

            var parameters = actionDescriptor
                .Parameters
                .OfType<ControllerParameterDescriptor>()
                .ToArray();

            this.Host = host;
            this.Protocol = protocol;
            this.Template = actionDescriptor.AttributeRouteInfo.Template.ToLower();
            this.Method = actionDescriptor.GetHttpMethod().ToUpper();
            this.ArgumentsCollection = new ArgumentCollection(parameters, arguments);

            this.BodyArgument = this.ArgumentsCollection.SingleOrDefault(argument => argument.BindingSource == BindingSource.Body);
        }

        public AuthorizationPolicy[] Policies { get; }
        public string Host { get; }
        public string Protocol { get; }
        public string Method { get; }
        public string Template { get; }
        public ActionArgument BodyArgument { get; }
        public ArgumentCollection ArgumentsCollection { get; }

        private static AuthorizationPolicy[] ComputePolicies(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor
                .FilterDescriptors
                .Select(f => f.Filter)
                .OfType<AuthorizeFilter>()
                .Select(filter => filter.Policy)
                .ToArray();
        }
    }
}