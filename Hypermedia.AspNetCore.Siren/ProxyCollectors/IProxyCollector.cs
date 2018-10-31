using System;

namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    internal interface IProxyCollector
    {
        EndpointDescriptor GetEndpointDescriptor<T>(Action<T> select) where T : class;
    }
}
