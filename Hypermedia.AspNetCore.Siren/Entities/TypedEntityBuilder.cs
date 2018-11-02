using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    internal class TypedEntityBuilder<T> : EntityBuilder, ITypedEntityBuilder<T> where T : class
    {
        public TypedEntityBuilder(
            IEndpointDescriptorProvider endpointDescriptorProvider,
            ClaimsPrincipal claimsPrincipal)
            : base(endpointDescriptorProvider, claimsPrincipal)
        {
        }

        public ITypedEntityBuilder<T> WithAction(string name, Action<T> @select)
        {
            WithAction<T>(name, @select);

            return this;
        }

        public ITypedEntityBuilder<T> WithEntity(Action<T> select, params string[] classes)
        {
            WithEntity<T>(select, classes);

            return this;
        }

        public ITypedEntityBuilder<T> WithEntity(Action<IEntityBuilder> select)
        {
            WithEntity<T>(select);

            return this;
        }
        
        public ITypedEntityBuilder<T> WithLink(string name, Action<T> @select, params string[] rel)
        {
            WithLink<T>(name, @select, rel);

            return this;
        }

        public ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne, params string[] classes)
        {
            base.WithEntities(enumerable, configureOne, classes);

            return this;
        }

        public new ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne)
        {
            base.WithEntities(enumerable, configureOne);

            return this;
        }

        public ITypedEntityBuilder<T> WithLinks(string[] rel, Dictionary<string, Action<T>> links)
        {
            base.WithLinks(rel, links);

            return this;
        }

        public ITypedEntityBuilder<T> WithLinks(Dictionary<string, Action<T>> links)
        {
            base.WithLinks(links);

            return this;
        }

        public new ITypedEntityBuilder<T> WithClasses(params string[] classes)
        {
            base.WithClasses(classes);

            return this;
        }

        public new ITypedEntityBuilder<T> WithProperties(object properties)
        {
            base.WithProperties(properties);

            return this;
        }
    }
}
