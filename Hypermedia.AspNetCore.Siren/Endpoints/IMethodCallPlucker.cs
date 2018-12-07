namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal interface IMethodCallPlucker
    {
        void PluckMethodCall<T>(Expression<Action<T>> expression, out (MethodInfo Method, object[] Arguments) methodCall) where T : class;
    }
}
