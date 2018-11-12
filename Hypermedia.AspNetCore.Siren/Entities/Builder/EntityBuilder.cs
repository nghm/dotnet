namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using Actions;
    using AutoMapper;
    using Endpoints;
    using Links;

    internal partial class EntityBuilder : IEntityBuilder
    {
        private readonly ISet<string> _classes = new HashSet<string>();
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly IList<IEntity> _entities = new List<IEntity>();
        private readonly IList<ILink> _links = new List<ILink>();
        private readonly IList<IAction> _actions = new List<IAction>();

        private readonly IMapper _mapper;
        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly IHrefFactory _hrefFactory;
        private readonly IAccessValidator _accessValidator;
        private readonly IFieldsFactory _fieldsFactory;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public EntityBuilder(
            IMapper mapper,
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            IFieldsFactory fieldsFactory,
            IAccessValidator accessValidator,
            ClaimsPrincipal claimsPrincipal)
        {
            this._mapper = mapper;
            this._endpointDescriptorProvider = endpointDescriptorProvider;
            this._hrefFactory = hrefFactory;
            this._accessValidator = accessValidator;
            this._fieldsFactory = fieldsFactory;
            this._claimsPrincipal = claimsPrincipal;
        }

        public IEntityBuilder With<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T : class
        {
            var builder = TypedEmptyClone<T>();

            entityBuilderConfiguration.Invoke(builder);

            builder.BuildOver(this);

            return this;
        }
    }
}
