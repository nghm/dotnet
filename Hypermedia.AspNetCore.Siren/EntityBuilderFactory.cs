namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using Entities;
    using ProxyCollectors;

    internal class EntityBuilderFactory : IEntityBuilderFactory
    {
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;

        public EntityBuilderFactory(IEndpointDescriptorProvider endpointDescriptorProvider)
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
