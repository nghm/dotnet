using Castle.DynamicProxy;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Dynamic;

    internal class CachedProxyCollector : IProxyCollector
    {
        private readonly IDictionary<Type, object> _cache = new Dictionary<Type, object>();

        private readonly ProxyGenerator _generator = new ProxyGenerator();
        private readonly AutoMocker _mocker = new AutoMocker();
        private readonly IControllerTypeChecker _controllerTypeChecker;
        private readonly IActionDescriptorResolver _actionDescriptorResolver;

        public CachedProxyCollector(
            IControllerTypeChecker controllerTypeChecker,
            IActionDescriptorResolver actionDescriptorResolver
        )
        {
            this._controllerTypeChecker = controllerTypeChecker;
            this._actionDescriptorResolver = actionDescriptorResolver;
        }
        
        public CollectedMethodCall ProxyCollectOne<T>(Action<T> select) where T : class
        {
            var type = typeof(T);

            if (!this._cache.ContainsKey(type))
            {
                var interceptor = new CallCollectorInterceptor();
                var ctor = type.GetConstructors().First();
                var arguments = ctor
                    .GetParameters()
                    .Select(parameter => this._mocker.Get(parameter.ParameterType))
                    .ToArray();

                this._cache[type] = this._generator.CreateClassProxy(type, arguments, interceptor);
            }

            var proxy = this._cache[type];

            try
            {
                select.Invoke(proxy as T);
            }
            catch (CallCollectionFinishedException collectionResult)
            {
                return collectionResult.Call;
            }

            return null;
        }
        
        public EndpointDescriptor GetEndpointDescriptor<T>(Action<T> select) where T : class
        {
            var methodCall = ProxyCollectOne(select);

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