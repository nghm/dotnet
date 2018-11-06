namespace Hypermedia.AspNetCore.Siren.Entities
{
    using Links;
    using ProxyCollectors;
    using Actions;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Linq.Expressions;
    using Newtonsoft.Json.Linq;

    internal class EntityBuilder : IEntityBuilder
    {
        private readonly ISet<string> _classes = new HashSet<string>();
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly IList<IEntity> _entities = new List<IEntity>();
        private readonly IList<ILink> _links = new List<ILink>();
        private readonly IList<IAction> _actions = new List<IAction>();

        private readonly IEndpointDescriptorProvider _endpointDescriptorProvider;
        private readonly ClaimsPrincipal _claimsPrincipal;

        public EntityBuilder(
            IEndpointDescriptorProvider endpointDescriptorProvider, 
            ClaimsPrincipal user
        )
        {
            this._endpointDescriptorProvider = endpointDescriptorProvider;
            this._claimsPrincipal = user;
        }

        public IEntity Build()
        {
            return new Entity(
                this._classes.ToArray(),
                this._entities.ToArray(),
                this._links.ToArray(),
                this._properties,
                this._actions.ToArray()
            );
        }

        private void BuildOver(EntityBuilder entityBuilder)
        {
            this._classes.ToList()
                .ForEach(@class => entityBuilder._classes.Add(@class));
            this._links.ToList()
                .ForEach(link => entityBuilder._links.Add(link));
            this._entities.ToList()
                .ForEach(entity => entityBuilder._entities.Add(entity));
            this._properties.ToList()
                .ForEach(property => entityBuilder._properties.Add(property));
            this._actions.ToList()
                .ForEach(action => entityBuilder._actions.Add(action));
        }
        
        public IEntityBuilder WithLink<T>(string name, Expression<Action<T>> @select, params string[] rel) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(@select);

            if (descriptor == null) {
                return this;
            }

            if (!descriptor.CanAccess(this._claimsPrincipal))
            {
                return this;
            }

            if (!descriptor.IsLink())
            {
                return this;
            }

            this._links.Add(new Link(name, descriptor.Href, rel));

            return this;
        }

        public IEntityBuilder WithLinks<T>(string[] rel, IDictionary<string, Expression<Action<T>>> links) where T : class
        {
            foreach(var link in links)
            {
                WithLink(link.Key, link.Value, rel);
            }

            return this;
        }

        public IEntityBuilder WithLinks<T>(IDictionary<string, Expression<Action<T>>> links) where T : class
        {
            foreach (var link in links)
            {
                WithLink(link.Key, link.Value);
            }

            return this;
        }

        public IEntityBuilder WithClasses(params string[] classes)
        {
            foreach (var @class in classes)
            {
                this._classes.Add(@class);
            }

            return this;
        }

        public IEntityBuilder WithProperties<TProp>(TProp properties)
        {
            var propDictionary = JObject
                .FromObject(properties)
                .ToObject<IDictionary<string, object>>();

            return WithProperties(propDictionary.AsEnumerable());
        }

        private IEntityBuilder WithProperties(IEnumerable<KeyValuePair<string, object>> properties)
        {
            foreach (var property in properties)
            {
                this._properties.Add(property.Key, property.Value);
            }

            return this;
        }

        public IEntityBuilder WithAction<T>(string name, Expression<Action<T>> @select) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(@select);

            if (descriptor == null)
            {
                return this;
            }

            if (!descriptor.CanAccess(this._claimsPrincipal))
            {
                return this;
            }

            var method = descriptor.Method;
            
            var action = new Actions.Action(
                name,
                descriptor.Href,
                method,
                descriptor.Fields
            );

            this._actions.Add(action);

            return this;
        }

        public IEntityBuilder With<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T : class
        {
            var builder = new TypedEntityBuilder<T>(this._endpointDescriptorProvider, this._claimsPrincipal);

            entityBuilderConfiguration.Invoke(builder);

            builder.BuildOver(this);

            return this;
        }
        
        public IEntityBuilder WithEntity<T>(Expression<Action<T>> @select, params string[] classes) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(@select);

            if (descriptor == null)
            {
                return this;
            }

            if (!descriptor.CanAccess(this._claimsPrincipal))
            {
                return this;
            }

            var method = descriptor.Method;
            var href = descriptor.Href;

            if (method == "GET")
            {
                this._entities.Add(new Entity(classes, href));
            }

            return this;
        }

        public IEntityBuilder WithEntity<T>(Action<IEntityBuilder> configure) where T : class
        {
            var builder = new EntityBuilder(this._endpointDescriptorProvider, this._claimsPrincipal);

            configure.Invoke(builder);

            this._entities.Add(builder.Build());

            return this;
        }

        public IEntityBuilder WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne)
        {
            foreach(var enumeration in enumerable)
            {
                var builder = new EntityBuilder(this._endpointDescriptorProvider, this._claimsPrincipal);

                configureOne.Invoke(builder, enumeration);

                this._entities.Add(builder.Build());
            }

            return this;
        }

        public IEntityBuilder WithEntities<T, TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne, string[] classes) where T: class
        {
            foreach(var enumeration in enumerable)
            {
                this.WithEntity((T controller) => configureOne(controller, enumeration), classes);
            }

            return this;
        }
    }
}
