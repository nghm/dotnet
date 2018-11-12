namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq.Expressions;

    internal interface IEndpointDescriptorProvider
    {
        EndpointDescriptor GetEndpointDescriptor<T>(Expression<Action<T>> endpointCapture) where T : class;
    }
}