namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq.Expressions;

    internal interface ICallCollector
    {
        CollectedMethodCall CollectCall<T>(Expression<Action<T>> collect) where T : class;
    }
}
