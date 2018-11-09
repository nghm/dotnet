using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    using System.Linq.Expressions;
    using AutoMapper;
    using Builder;

    internal class TypedEntityBuilder<T> : EntityBuilder, ITypedEntityBuilder<T> where T : class
    {
        private readonly IHrefFactory _hrefFactory;

        public TypedEntityBuilder(
            IMapper mapper,
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            IAccessValidator accessValidator,
            IFieldsFactory fieldsFactory,
            ClaimsPrincipal claimsPrincipal)
            : base(mapper, endpointDescriptorProvider, hrefFactory, fieldsFactory, accessValidator, claimsPrincipal)
        {
            this._hrefFactory = hrefFactory;
        }

        public new ITypedEntityBuilder<T> WithProperties<TProp, TSource>(TSource properties)
        {
            base.WithProperties<TProp, TSource>(properties);

            return this;
        }

        public ITypedEntityBuilder<T> WithAction(string name, Expression<Action<T>> @select)
        {
            WithAction<T>(name, @select);

            return this;
        }

        public ITypedEntityBuilder<T> WithEntity(Expression<Action<T>> @select, params string[] classes)
        {
            WithEntity<T>(@select, classes);

            return this;
        }

        public new ITypedEntityBuilder<T> WithEntity(Action<IEntityBuilder> select)
        {
            base.WithEntity(select);

            return this;
        }
        
        public ITypedEntityBuilder<T> WithLink(string name, Expression<Action<T>> @select, params string[] rel)
        {
            WithLink<T>(name, @select, rel);

            return this;
        }

        public ITypedEntityBuilder<T> WithEntities<TM>(
            IEnumerable<TM> enumerable,
            Action<T, TM> configureOne, 
            params string[] classes)
        {
            base.WithEntities(enumerable, configureOne, classes);

            return this;
        }

        public new ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne)
        {
            base.WithEntities(enumerable, configureOne);

            return this;
        }

        public ITypedEntityBuilder<T> WithLinks(string[] rel, Dictionary<string, Expression<Action<T>>> links)
        {
            base.WithLinks(rel, links);

            return this;
        }

        public ITypedEntityBuilder<T> WithLinks(Dictionary<string, Expression<Action<T>>> links)
        {
            base.WithLinks(links);

            return this;
        }

        public new ITypedEntityBuilder<T> WithClasses(params string[] classes)
        {
            base.WithClasses(classes);

            return this;
        }

        public new ITypedEntityBuilder<T> WithProperties<TProp>(TProp properties)
        {
            base.WithProperties(properties);

            return this;
        }
    }
}
