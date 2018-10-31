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
        private ISet<string> classes = new HashSet<string>();
        private IDictionary<string, object> properties = new Dictionary<string, object>();
        private IList<IEntity> entities = new List<IEntity>();
        private IList<ILink> links = new List<ILink>();
        private IList<IAction> actions = new List<IAction>();

        private readonly IProxyCollector proxyCollector;
        private readonly ClaimsPrincipal claimsPrincipal;

        public EntityBuilder(
            IProxyCollector proxyCollector, 
            ClaimsPrincipal user
        )
        {
            this.proxyCollector = proxyCollector;
            claimsPrincipal = user;
        }

        public IEntity Build()
        {
            return new Entity(
                classes.ToArray(),
                entities.ToArray(),
                links.ToArray(),
                properties,
                actions.ToArray()
            );
        }
        
        public IEntityBuilder AddLink<T>(Action<T> select, params string[] rel) where T : class
        {
            var descriptor = proxyCollector.GetEndpointDescriptor(select);

            if (descriptor == null) {
                return this;
            }

            var method = descriptor.Method;
            var body = descriptor.Body;

            if (method == "GET" && body == null)
            {
                links.Add(new Link
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
            classes.ToList()
                .ForEach(@class => entityBuilder.classes.Add(@class));
            links.ToList()
                .ForEach(link => entityBuilder.links.Add(link));
            entities.ToList()
                .ForEach(entity => entityBuilder.entities.Add(entity));
            properties.ToList()
                .ForEach(property => entityBuilder.properties.Add(property));
            actions.ToList()
                .ForEach(action => entityBuilder.actions.Add(action));
        }

        public IEntityBuilder AddClasses(params string[] classes)
        {
            foreach (var @class in classes)
            {
                this.classes.Add(@class);
            }

            return this;
        }

        public IEntityBuilder AddProperties(object properties)
        {
            foreach (var property in properties.AsPropertyEnumerable())
            {
                this.properties.Add(property);
            }

            return this;
        }

        public IEntityBuilder AddAction<T>(Action<T> select, string name) where T : class
        {
            var descriptor = proxyCollector.GetEndpointDescriptor(select);

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

            actions.Add(action);

            return this;
        }

        public IEntityBuilder AddFrom<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T : class
        {
            var builder = new TypedEntityBuilder<T>(this.proxyCollector, this.claimsPrincipal);

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
            var descriptor = proxyCollector.GetEndpointDescriptor(select);

            if (descriptor == null)
            {
                return this;
            }

            var method = descriptor.Method;
            var href = descriptor.Href;

            if (method == "GET")
            {
                entities.Add(new Entity(classes, href));
            }

            return this;
        }

        public IEntityBuilder AddEntity<T>(Action<IEntityBuilder> configure) where T : class
        {
            var builder = new EntityBuilder(proxyCollector, claimsPrincipal);

            configure.Invoke(builder);

            entities.Add(builder.Build());

            return this;
        }

        public IEntityBuilder AddEntities<M>(IEnumerable<M> enumerable, Action<IEntityBuilder, M> configureOne)
        {
            foreach(var enumeration in enumerable)
            {
                var builder = new EntityBuilder(proxyCollector, claimsPrincipal);

                configureOne.Invoke(builder, enumeration);

                entities.Add(builder.Build());
            }

            return this;
        }

        public IEntityBuilder AddEntities<T, M>(IEnumerable<M> enumerable, Action<T, M> configureOne, string[] classes) where T: class
        {
            foreach(var enumeration in enumerable)
            {
                AddEntity<T>(controller => configureOne(controller, enumeration), classes);
            }

            return this;
        }
    }
}
