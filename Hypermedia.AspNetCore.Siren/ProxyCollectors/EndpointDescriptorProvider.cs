namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Linq.Expressions;
    using Actions;
    using Microsoft.AspNetCore.Authorization;

    internal class EndpointDescriptorProvider : IEndpointDescriptorProvider
    {
        private readonly IControllerTypeChecker _controllerTypeChecker;
        private readonly IActionDescriptorResolver _actionDescriptorResolver;
        private readonly IFieldMetadataProviderCollection _fieldMetadataProviderCollection;
        private readonly ICallCollector _callCollector;
        private readonly IAuthorizationService _authService;

        public EndpointDescriptorProvider(
            IControllerTypeChecker controllerTypeChecker,
            IActionDescriptorResolver actionDescriptorResolver,
            IFieldMetadataProviderCollection fieldMetadataProviderCollection,
            ICallCollector callCollector,
            IAuthorizationService authService)
        {
            this._controllerTypeChecker = controllerTypeChecker;
            this._actionDescriptorResolver = actionDescriptorResolver;
            this._fieldMetadataProviderCollection = fieldMetadataProviderCollection;
            this._callCollector = callCollector;
            this._authService = authService;
        }

        public EndpointDescriptor GetEndpointDescriptor<T>(Expression<Action<T>> @select) where T : class
        {
            var methodCall = this._callCollector.CollectCall(select);

            if (methodCall == null)
            {
                return null;
            }

            var arguments = methodCall.Arguments;
            var controllerType = methodCall.Target;
            var actionMethodInfo = methodCall.Method;

            var actionDescriptor = this._actionDescriptorResolver.Resolve(controllerType, actionMethodInfo);

            if (actionDescriptor == null)
            {
                throw new InvalidOperationException("Expression does not call application action");
            }

            return new EndpointDescriptor(this._authService, this._fieldMetadataProviderCollection, actionDescriptor, arguments, "localhost:54287", "http");
        }
    }
}