namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Castle.DynamicProxy;
    using Moq.AutoMock;

    internal class ExpressionProxyCollector : IProxyCollector
    {
        public CollectedMethodCall ProxyCollectOne<T>(Expression<Action<T>> select) where T : class
        {
            if (select.Body is MethodCallExpression methodCall)
            {
                return new CollectedMethodCall
                {
                    Arguments = methodCall
                        .Arguments
                        .Select(argument => Expression.Lambda(argument).Compile().DynamicInvoke())
                        .ToArray(),
                    Method = methodCall.Method,
                    Target = methodCall.Method.ReflectedType
                };
            }

            throw new InvalidOperationException("Expression is not a method call!");
        }
    }
}