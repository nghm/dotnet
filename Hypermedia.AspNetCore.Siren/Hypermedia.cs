using Hypermedia.AspNetCore.Siren.Entities;
using Hypermedia.AspNetCore.Siren.ProxyCollectors;

namespace Hypermedia.AspNetCore.Siren
{
    class Hypermedia : IHypermedia
    {
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;

        public Hypermedia(IEndpointDescriptorProvider endpointDescriptorProvider)
        {
            this._endpointDescriptorProvider = endpointDescriptorProvider;
        }

        public IEntityBuilder MakeEntity()
        {
            return new EntityBuilder(this._endpointDescriptorProvider, null);
        }
    }
}
