using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Hypermedia.AspNetCore.Siren.Links;
using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using Hypermedia.AspNetCore.Siren.Util;
using Hypermedia.AspNetCore.Siren.Actions;

namespace Hypermedia.AspNetCore.Siren.Entities
{
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
        
        public IEntityBuilder AddLink<T>(Action<T> select, params string[] rel) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(select);

            if (descriptor == null) {
                return this;
            }

            if (descriptor.IsLink())
            {
                this._links.Add(new Link
                {
                    Href = descriptor.Href,
                    Rel = rel
                });
            }

            return this;
        }

        public IEntityBuilder AddLinks<T>(string[] rel, params Action<T>[] selectors) where T : class
        {
            foreach(var selector in selectors)
            {
                AddLink(selector, rel);
            }

            return this;
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

        public IEntityBuilder AddClasses(params string[] classes)
        {
            foreach (var @class in classes)
            {
                this._classes.Add(@class);
            }

            return this;
        }

        public IEntityBuilder AddProperties(object properties)
        {
            foreach (var property in properties.AsPropertyEnumerable())
            {
                this._properties.Add(property);
            }

            return this;
        }

        public IEntityBuilder AddAction<T>(Action<T> select, string name) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(select);

            if (descriptor == null)
            {
                return this;
            }

            var method = descriptor.Method;
            
            var action = new Actions.Action
            {
                Name = name,
                Href = descriptor.Href,
                Method = method,
                Fields = descriptor.Fields
            };

            this._actions.Add(action);

            return this;
        }

        public IEntityBuilder AddFrom<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T : class
        {
            var builder = new TypedEntityBuilder<T>(this._endpointDescriptorProvider, this._claimsPrincipal);

            entityBuilderConfiguration.Invoke(builder);

            builder.BuildOver(this);

            return this;
        }

        public IEntityBuilder AddProperties<T>(T properties)
        {
            AddProperties(properties as object);

            return this;
        }

        public IEntityBuilder AddEntity<T>(Action<T> select, params string[] classes) where T : class
        {
            var descriptor = this._endpointDescriptorProvider.GetEndpointDescriptor(select);

            if (descriptor == null)
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

        public IEntityBuilder AddEntity<T>(Action<IEntityBuilder> configure) where T : class
        {
            var builder = new EntityBuilder(this._endpointDescriptorProvider, this._claimsPrincipal);

            configure.Invoke(builder);

            this._entities.Add(builder.Build());

            return this;
        }

        public IEntityBuilder AddEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne)
        {
            foreach(var enumeration in enumerable)
            {
                var builder = new EntityBuilder(this._endpointDescriptorProvider, this._claimsPrincipal);

                configureOne.Invoke(builder, enumeration);

                this._entities.Add(builder.Build());
            }

            return this;
        }

        public IEntityBuilder AddEntities<T, TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne, string[] classes) where T: class
        {
            foreach(var enumeration in enumerable)
            {
                AddEntity<T>(controller => configureOne(controller, enumeration), classes);
            }

            return this;
        }
    }
}
