using System;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Linq.Expressions;

    internal interface ICallCollector
    {
        CollectedMethodCall CollectCall<T>(Expression<Action<T>> collect) where T : class;
    }
}
