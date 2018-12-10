namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using System;
    using System.Linq.Expressions;

    internal interface IProxifiedActions
    {
        IProxifiedAction Get<TController>(Expression<Action<TController>> callExpression)
            where TController : class;
    }
}