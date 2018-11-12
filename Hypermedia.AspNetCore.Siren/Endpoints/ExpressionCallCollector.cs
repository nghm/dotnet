namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    internal class ExpressionCallCollector : ICallCollector
    {
        public CollectedMethodCall CollectCall<T>(Expression<Action<T>> select) where T : class
        {
            if (select.Body is MethodCallExpression methodCall)
            {
                return new CollectedMethodCall(
                    methodCall
                        .Arguments
                        .Select(argument => Expression.Lambda(argument).Compile().DynamicInvoke())
                        .ToArray(),
                    methodCall.Method,
                    methodCall.Method.ReflectedType
                );
            }

            throw new InvalidOperationException("Expression is not a method call!");
        }
    }
}