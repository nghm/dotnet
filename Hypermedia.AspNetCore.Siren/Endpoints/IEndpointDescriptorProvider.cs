namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Linq.Expressions;
    using System.Security.Claims;

    internal interface IEndpointDescriptorProvider
    {
        EndpointDescriptor GetEndpointDescriptor<T>(
            Expression<Action<T>> endpointCapture,
            ClaimsPrincipal claimsPrincipal
        ) where T : class;
    }
}