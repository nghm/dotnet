﻿namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using Castle.DynamicProxy;

    internal class CallCollectorInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var call = new CollectedMethodCall
            {
                Arguments = invocation.Arguments,
                Method = invocation.GetConcreteMethod(),
                Target = invocation.TargetType
            };

            throw new CallCollectionFinishedException(call);
        }
    }
}