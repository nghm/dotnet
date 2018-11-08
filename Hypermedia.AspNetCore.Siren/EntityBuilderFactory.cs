namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using AutoMapper;
    using Entities;
    using ProxyCollectors;

    internal class EntityBuilderFactory : IEntityBuilderFactory
    {
        private readonly IMapper _mapper;
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefGenerator _hrefGenerator;

        public EntityBuilderFactory(
            IMapper mapper,
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefGenerator hrefGenerator
        )
        {
            this._mapper = mapper;
            this._endpointDescriptorProvider = endpointDescriptorProvider;
            this._hrefGenerator = hrefGenerator;
        }

        public EntityBuilder MakeEntity()
        {
            return MakeEntity(null);
        }

        public EntityBuilder MakeEntity(ClaimsPrincipal user)
        {
            return new EntityBuilder(this._mapper, this._endpointDescriptorProvider, this._hrefGenerator, user);
        }
    }
}
