namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;

    internal interface IEndpointDescriptorProvider
    {
        EndpointDescriptor GetEndpointDescriptor<T>(Action<T> select) where T : class;
    }
}