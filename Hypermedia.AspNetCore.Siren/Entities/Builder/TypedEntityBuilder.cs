namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Security.Claims;
    using AutoMapper;
    using Endpoints;

    internal class TypedEntityBuilder<T> : EntityBuilder, ITypedEntityBuilder<T> where T : class
    {
        private readonly IHrefFactory _hrefFactory;

        public TypedEntityBuilder(
            IMapper mapper,
            IEndpointDescriptorProvider endpointDescriptorProvider,
            IHrefFactory hrefFactory,
            IFieldsFactory fieldsFactory,
            ClaimsPrincipal claimsPrincipal)
            : base(mapper, endpointDescriptorProvider, hrefFactory, fieldsFactory, claimsPrincipal)
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

        public ITypedEntityBuilder<T> WithAction<TBody>(
            string name, 
            Expression<Action<T>> endpointCapture, 
            Action<IActionBuilder<TBody>> configureAction) 
            where TBody : class
        {
            WithAction<T, TBody>(name, endpointCapture, configureAction);

            return this;
        }

        public ITypedEntityBuilder<T> WithEntity(Expression<Action<T>> collectEntity, params string[] classes)
        {
            WithEntity<T>(collectEntity, classes);

            return this;
        }

        public new ITypedEntityBuilder<T> WithEntity(Action<IEntityBuilder> configure)
        {
            base.WithEntity(configure);

            return this;
        }
        
        public ITypedEntityBuilder<T> WithLink(string name, Expression<Action<T>> captureLink, params string[] rel)
        {
            WithLink<T>(name, captureLink, rel);

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
