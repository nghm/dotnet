using System;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal interface IProxyCollector
    {
        CollectedMethodCall ProxyCollectOne<T>(Action<T> collect) where T : class;
    }
}
