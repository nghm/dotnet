namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Actions;
    using Links;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IApiAwareEntityBuilder
    {
        IEntityBuilder WithAction<T>(string name, Expression<Action<T>> endpointCapture) where T : class;

        IEntityBuilder WithAction<T, TBody>(string name, Expression<Action<T>> endpointCapture, Action<IActionBuilder<TBody>> actionBuilder)
            where T : class
            where TBody : class;

        IEntityBuilder WithEntity<T>(Expression<Action<T>> endpointCapture, params string[] classes) where T : class;
        IEntityBuilder WithLink<T>(string name, Expression<Action<T>> endpointCapture, params string[] rel) where T : class;
        IEntityBuilder WithLinks<T>(string[] rel, IDictionary<string, Expression<Action<T>>> links) where T : class;
        IEntityBuilder WithLinks<T>(IDictionary<string, Expression<Action<T>>> links) where T : class;
    }

    public interface IEntityBuilder : IApiAwareEntityBuilder
    {
        IEntityBuilder WithClasses(params string[] classes);
        IEntityBuilder WithProperties<TProp>(TProp properties);
        IEntityBuilder WithProperties<TProp, TSource>(TSource properties);

        IEntityBuilder WithAction(IAction action);

        IEntityBuilder WithEntity(IEntity entity);
        IEntityBuilder WithEntity(Action<IEntityBuilder> builder);
        IEntityBuilder WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne);

        IEntityBuilder WithEntities<T, TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne, string[] classes) where T : class;

        IEntityBuilder WithLink(ILink link);

        IEntityBuilder With<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T : class;

        IEntity Build();
    }
}
