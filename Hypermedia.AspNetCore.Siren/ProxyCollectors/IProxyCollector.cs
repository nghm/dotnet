using System;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Linq.Expressions;

    internal interface IProxyCollector
    {
        CollectedMethodCall ProxyCollectOne<T>(Expression<Action<T>> collect) where T : class;
    }
}
