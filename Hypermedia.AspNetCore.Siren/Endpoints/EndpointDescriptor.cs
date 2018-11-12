namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System.Collections.Generic;
    using System.Linq;
    using Actions.Fields;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.Authorization;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Util;

    internal class EndpointDescriptor
    {

        public EndpointDescriptor(
            ControllerActionDescriptor actionDescriptor,
            object[] arguments,
            string host,
            string protocol)
        {
            this.Policies = this.ComputePolicies(actionDescriptor);

            var parameters = actionDescriptor
                .Parameters
                .OfType<ControllerParameterDescriptor>()
                .ToArray();

            this.Host = host;
            this.Protocol = protocol;
            this.Body = actionDescriptor.PickBodyArgument(arguments);
            this.Template = actionDescriptor.AttributeRouteInfo.Template.ToLower();
            this.Method = actionDescriptor.GetHttpMethod().ToUpper();
            this.ArgumentsCollection = new ArgumentsCollection(parameters, arguments);

            this.BodyArgument = this.ArgumentsCollection.SingleOrDefault(argument => argument.Key.BindingInfo.BindingSource.Id == "Body");
        }

        public AuthorizationPolicy[] Policies { get; }
        public string Host { get; }
        public string Protocol { get; }
        public string Method { get; }
        public object Body { get; }
        public string Template { get; }
        public KeyValuePair<ControllerParameterDescriptor, object> BodyArgument { get; }
        public ArgumentsCollection ArgumentsCollection { get; }

        private AuthorizationPolicy[] ComputePolicies(
            ControllerActionDescriptor actionDescriptor
        )
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