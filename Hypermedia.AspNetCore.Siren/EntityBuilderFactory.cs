namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using AutoMapper;
    using Entities;
    using Entities.Builder;
    using ProxyCollectors;

    internal class EntityBuilderFactory : IEntityBuilderFactory
    {
        private readonly IMapper _mapper;
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefFactory _hrefFactory;
        private readonly IAccessValidator _accessValidator;
        private readonly IFieldsFactory _fieldsFactory;

        public EntityBuilderFactory(
            IMapper mapper,
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            IAccessValidator accessValidator, 
            IFieldsFactory fieldsFactory)
        {
            this._mapper = mapper;
            this._endpointDescriptorProvider = endpointDescriptorProvider;
            this._hrefFactory = hrefFactory;
            this._accessValidator = accessValidator;
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
                this._accessValidator, 
                user);
        }
    }
}
