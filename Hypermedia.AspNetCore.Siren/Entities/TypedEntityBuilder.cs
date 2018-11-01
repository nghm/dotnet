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

        public ITypedEntityBuilder<T> AddAction(Action<T> select, string name)
        {
            AddAction<T>(select, name);

            return this;
        }

        public ITypedEntityBuilder<T> AddEntity(Action<T> select, params string[] classes)
        {
            AddEntity<T>(select, classes);

            return this;
        }

        public ITypedEntityBuilder<T> AddEntity(Action<IEntityBuilder> select)
        {
            AddEntity<T>(select);

            return this;
        }
        
        public ITypedEntityBuilder<T> AddLink(Action<T> select, params string[] rel)
        {
            AddLink<T>(select, rel);

            return this;
        }

        public ITypedEntityBuilder<T> AddEntities<M>(IEnumerable<M> enumerable, Action<T, M> configureOne, params string[] classes)
        {
            base.AddEntities(enumerable, configureOne, classes);

            return this;
        }

        public new ITypedEntityBuilder<T> AddEntities<M>(IEnumerable<M> enumerable, Action<IEntityBuilder, M> configureOne)
        {
            base.AddEntities(enumerable, configureOne);

            return this;
        }

    }
}
