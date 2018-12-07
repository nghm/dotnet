namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;

    internal class EndpointDescriptorProvider : IEndpointDescriptorProvider
    {
        private readonly IActionDescriptorResolver _actionDescriptorResolver;
        private readonly IMethodCallPlucker _methodCallPlucker;
        private readonly IAccessValidator _accessValidator;

        public EndpointDescriptorProvider(
            IActionDescriptorResolver actionDescriptorResolver,
            IMethodCallPlucker methodCallPlucker,
            IAccessValidator accessValidator)
        {
            this._actionDescriptorResolver = actionDescriptorResolver;
            this._methodCallPlucker = methodCallPlucker;
            this._accessValidator = accessValidator;
        }

        public IEndpointDescriptor GetEndpointDescriptor<T>(
            Expression<Action<T>> endpointCapture,
            ClaimsPrincipal claimsPrincipal
        ) where T : class
        {
            this._methodCallPlucker.PluckMethodCall(endpointCapture, out var call);

            var (method, arguments) = call;

            var actionDescriptor = this._actionDescriptorResolver.Resolve(method);

            if (actionDescriptor == null)
            {
                throw new InvalidOperationException("Expression does not call application action");
            }

            var endpointDescriptor = new EndpointDescriptor(actionDescriptor, arguments, "localhost:54287", "http");

            if (!this._accessValidator.CanAccess(claimsPrincipal, endpointDescriptor.Policies))
            {
                return null;
            }

            return endpointDescriptor;
        }
    }
}