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

        public EntityBuilder MakeEntity()
        {
            return MakeEntity(null);
        }

        public EntityBuilder MakeEntity(ClaimsPrincipal user)
        {
            return new EntityBuilder(this._endpointDescriptorProvider, user);
        }
    }
}
