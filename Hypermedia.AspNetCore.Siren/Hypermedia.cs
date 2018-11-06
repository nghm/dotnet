using Hypermedia.AspNetCore.Siren.Entities;
using Hypermedia.AspNetCore.Siren.ProxyCollectors;

namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;

    class Hypermedia : IHypermedia
    {
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;

        public Hypermedia(IEndpointDescriptorProvider endpointDescriptorProvider)
        {
            this._endpointDescriptorProvider = endpointDescriptorProvider;
        }

        public IEntityBuilder MakeEntity()
        {
            return MakeEntity(null);
        }

        public IEntityBuilder MakeEntity(ClaimsPrincipal user)
        {
            return new EntityBuilder(this._endpointDescriptorProvider, user);
        }
    }
}
