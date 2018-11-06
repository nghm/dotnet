namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;

    internal class EndpointDescriptorProvider : IEndpointDescriptorProvider
    {
        private readonly IControllerTypeChecker _controllerTypeChecker;
        private readonly IActionDescriptorResolver _actionDescriptorResolver;
        private readonly IProxyCollector _proxyCollector;

        public EndpointDescriptorProvider(
            IControllerTypeChecker controllerTypeChecker,
            IActionDescriptorResolver actionDescriptorResolver,
            IProxyCollector proxyCollector
        )
        {
            this._controllerTypeChecker = controllerTypeChecker;
            this._actionDescriptorResolver = actionDescriptorResolver;
            this._proxyCollector = proxyCollector;
        }

        public EndpointDescriptor GetEndpointDescriptor<T>(Action<T> select) where T : class
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

            return new EndpointDescriptor(actionDescriptor, arguments, "localhost:54287", "http");
        }
    }
}