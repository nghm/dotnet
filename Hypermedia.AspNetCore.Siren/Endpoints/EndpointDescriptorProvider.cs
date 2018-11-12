namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    internal class EndpointDescriptorProvider : IEndpointDescriptorProvider
    {
        private readonly IActionDescriptorResolver _actionDescriptorResolver;
        private readonly ICallCollector _callCollector;
        private readonly IAccessValidator _accessValidator;

        public EndpointDescriptorProvider(
            IActionDescriptorResolver actionDescriptorResolver,
            ICallCollector callCollector,
            IAccessValidator accessValidator,
            IAuthorizationService authService)
        {
            this._actionDescriptorResolver = actionDescriptorResolver;
            this._callCollector = callCollector;
            this._accessValidator = accessValidator;
        }

        public EndpointDescriptor GetEndpointDescriptor<T>(
            Expression<Action<T>> endpointCapture,
            ClaimsPrincipal claimsPrincipal
        ) where T : class
        {
            var methodCall = this._callCollector.CollectMethodCall(endpointCapture);

            if (methodCall == null)
            {
                return null;
            }
            
            var actionDescriptor = this._actionDescriptorResolver.Resolve(methodCall.Target, methodCall.Method);

            if (actionDescriptor == null)
            {
                throw new InvalidOperationException("Expression does not call application action");
            }

            var endpointDescriptor = new EndpointDescriptor(actionDescriptor, methodCall.Arguments, "localhost:54287", "http");

            if (!this._accessValidator.CanAccess(claimsPrincipal, endpointDescriptor.Policies))
            {
                return null;
            }

            return endpointDescriptor;
        }
    }
}