namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class MethodMethodCallPlucker : IMethodCallPlucker
    {
        public void PluckMethodCall<T>(Expression<Action<T>> expression, out (MethodInfo Method, object[] Arguments) call) where T : class
        {
            if (!(expression.Body is MethodCallExpression body))
            {
                throw new InvalidOperationException("Expression is not a method call!");
            }

            call = (
                body.Method,
                body.Arguments
                    .Select(argument => Expression.Lambda(argument).Compile().DynamicInvoke())
                    .ToArray()
            );
        }
    }
}