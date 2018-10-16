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
    internal class CachedProxyCollector
    {
        public IDictionary<TypeInfo, object> CachedControllerProxyCollectors { get; }
        private readonly ProxyGenerator generator = new ProxyGenerator();
        private readonly AutoMocker mocker = new AutoMocker();
        private readonly IActionDescriptorCollectionProvider actionDescriptorCollectionProvider;

        public CachedProxyCollector(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            CachedControllerProxyCollectors = actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .GroupBy(descriptor => descriptor.ControllerTypeInfo)
                .ToDictionary(
                    group => group.First().ControllerTypeInfo,
                    group => MakeProxyControllerCollector(group.First())
                );
            this.actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        private object MakeProxyControllerCollector(ControllerActionDescriptor descriptor)
        {
            var interceptor = new CallCollectorInterceptor();
            var type = descriptor.ControllerTypeInfo.AsType();
            var ctor = type.GetConstructors().First();
            var arguments = ctor
                .GetParameters()
                .Select(parameter => mocker.Get(parameter.ParameterType))
                .ToArray();

            var proxy = generator.CreateClassProxy(type, arguments, interceptor);

            return proxy;
        }

        private CollectedMethodCall ProxyCollectOne<T>(Action<T> select) where T : class
        {
            Type controllerType = typeof(T);
            var proxyController = CachedControllerProxyCollectors[controllerType.GetTypeInfo()];

            try
            {
                select(proxyController as T);
            }
            catch (CallCollectionFinishedException collectionResult)
            {
                return collectionResult.Call;
            }

            return null;
        }

        private ActionDescriptor GetActionDescriptor(Type controllerType, MethodInfo methodInfo)
        {
            var actionDescriptor = actionDescriptorCollectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .First(ad => ad.ControllerTypeInfo == controllerType.GetTypeInfo() && ad.MethodInfo == methodInfo);

            return actionDescriptor;
        }

        internal EndpointDescriptor GetEndpointDescriptor<T>(Action<T> select) where T : class
        {
            var methodCall = ProxyCollectOne(select);

            if (methodCall == null)
            {
                return null;
            }

            var arguments = methodCall.Arguments;
            var controllerType = methodCall.Target;
            var actionMethodInfo = methodCall.Method;

            return new EndpointDescriptor(GetActionDescriptor(controllerType, actionMethodInfo), arguments, "localhost:54287", "http");
        }
    }
}