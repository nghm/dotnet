namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Linq.Expressions;
    using Microsoft.AspNetCore.Authorization;

    internal class EndpointDescriptorProvider : IEndpointDescriptorProvider
    {
        private readonly IControllerTypeChecker _controllerTypeChecker;
        private readonly IActionDescriptorResolver _actionDescriptorResolver;
        private readonly IProxyCollector _proxyCollector;
        private readonly IAuthorizationService _authService;

        public EndpointDescriptorProvider(
            IControllerTypeChecker controllerTypeChecker,
            IActionDescriptorResolver actionDescriptorResolver,
            IProxyCollector proxyCollector,
            IAuthorizationService authService)
        {
            this._controllerTypeChecker = controllerTypeChecker;
            this._actionDescriptorResolver = actionDescriptorResolver;
            this._proxyCollector = proxyCollector;
            this._authService = authService;
        }

        public EndpointDescriptor GetEndpointDescriptor<T>(Expression<Action<T>> @select) where T : class
        {
            var methodCall = this._proxyCollector.ProxyCollectOne(select);

            if (methodCall == null)
            {
                return null;
            }

            var arguments = methodCall.Arguments;
            var controllerType = methodCall.Target;
            var actionMethodInfo = methodCall.Method;

            var actionDescriptor = this._actionDescriptorResolver.Resolve(controllerType, actionMethodInfo);

            return new EndpointDescriptor(this._authService, actionDescriptor, arguments, "localhost:54287", "http");
        }
    }
}