namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Parallel;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IApiAwareEntityBuilder : IAsyncBuilder<IEntity>
    {
        IApiAwareEntityBuilder WithAction<T, TBody>(
            string name,
            Expression<Action<T>> endpointCapture,
            Action<IActionBuilder<TBody>> configureActionBuilder = null)
            where T : class
            where TBody : class;

        IApiAwareEntityBuilder WithEntity<T>(
            Expression<Action<T>> endpointCapture,
            params string[] classes)
            where T : class;

        IApiAwareEntityBuilder WithLink<T>(
            string name,
            Expression<Action<T>> endpointCapture,
            params string[] rel)
            where T : class;

        IApiAwareEntityBuilder WithLinks<T>(
            string[] rel,
            IDictionary<string, Expression<Action<T>>> links)
            where T : class;

        IApiAwareEntityBuilder WithLinks<T>(
            IDictionary<string, Expression<Action<T>>> links)
            where T : class;

        IApiAwareEntityBuilder WithEntity(
            Action<IApiAwareEntityBuilder> configureBuilder);

        IApiAwareEntityBuilder WithEntities<TM>(
            IEnumerable<TM> enumerable,
            Action<IApiAwareEntityBuilder, TM> configureOne);

        IApiAwareEntityBuilder WithEntities<T, TM>(
            IEnumerable<TM> enumerable,
            Action<T, TM> configureOne,
            string[] classes) where T : class;

        IApiAwareEntityBuilder WithProperties<TProps, TSource>(TSource properties);
        IApiAwareEntityBuilder WithProperties<TProps>(TProps properties);

        IApiAwareEntityBuilder WithClasses(params string[] classes);
    }
}