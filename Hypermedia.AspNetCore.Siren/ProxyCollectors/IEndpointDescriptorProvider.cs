namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Linq.Expressions;

    internal interface IEndpointDescriptorProvider
    {
        EndpointDescriptor GetEndpointDescriptor<T>(Expression<Action<T>> @select) where T : class;
    }
}