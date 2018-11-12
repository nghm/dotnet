namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using AutoMapper;
    using Endpoints;
    using Entities.Builder;

    internal class EntityBuilderFactory : IEntityBuilderFactory
    {
        private readonly IMapper _mapper;
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefFactory _hrefFactory;
        private readonly IFieldsFactory _fieldsFactory;

        public EntityBuilderFactory(
            IMapper mapper,
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            IFieldsFactory fieldsFactory)
        {
            this._mapper = mapper;
            this._endpointDescriptorProvider = endpointDescriptorProvider;
            this._hrefFactory = hrefFactory;
            this._fieldsFactory = fieldsFactory;
        }

        public EntityBuilder MakeEntity()
        {
            return MakeEntity(null);
        }

        public EntityBuilder MakeEntity(ClaimsPrincipal user)
        {
            return new EntityBuilder(
                this._mapper, 
                this._endpointDescriptorProvider, 
                this._hrefFactory, 
                this._fieldsFactory,
                user);
        }
    }
}
