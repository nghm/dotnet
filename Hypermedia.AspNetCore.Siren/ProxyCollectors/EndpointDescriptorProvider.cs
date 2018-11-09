﻿namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Linq.Expressions;
    using Actions;
    using Microsoft.AspNetCore.Authorization;

    internal class EndpointDescriptorProvider : IEndpointDescriptorProvider
    {
        private readonly IActionDescriptorResolver _actionDescriptorResolver;
        private readonly IFieldMetadataProviderCollection _fieldMetadataProviderCollection;
        private readonly ICallCollector _callCollector;
        private readonly IAuthorizationService _authService;

        public EndpointDescriptorProvider(
            IActionDescriptorResolver actionDescriptorResolver,
            IFieldMetadataProviderCollection fieldMetadataProviderCollection,
            ICallCollector callCollector,
            IAuthorizationService authService)
        {
            this._actionDescriptorResolver = actionDescriptorResolver;
            this._fieldMetadataProviderCollection = fieldMetadataProviderCollection;
            this._callCollector = callCollector;
            this._authService = authService;
        }

        public EndpointDescriptor GetEndpointDescriptor<T>(Expression<Action<T>> endpointCapture) where T : class
        {
            var methodCall = this._callCollector.CollectCall(endpointCapture);

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

            return new EndpointDescriptor(actionDescriptor, arguments, "localhost:54287", "http");
        }
    }
}