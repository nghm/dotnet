namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using System;
    using System.Linq.Expressions;

    internal class ProxifiedActions : IProxifiedActions
    {
        private readonly IMethodCallPlucker _methodCallPlucker;
        private readonly IProxifiedActionFactories _proxifiedActionFactories;

        public ProxifiedActions(IMethodCallPlucker methodCallPlucker, IProxifiedActionFactories factories)
        {
            this._methodCallPlucker = methodCallPlucker;
            this._proxifiedActionFactories = factories;
        }

        public IProxifiedAction Get<TController>(Expression<Action<TController>> callExpression)
            where TController : class
        {
            this._methodCallPlucker.PluckMethodCall(callExpression, out var call);

            var (method, arguments) = call;

            return this._proxifiedActionFactories
                .Get(method)
                .Make(arguments);
        }
    }
}