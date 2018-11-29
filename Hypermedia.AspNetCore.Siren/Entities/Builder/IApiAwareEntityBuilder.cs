namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Parallel;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IApiAwareEntityBuilder : IAsyncBuilder<IEntity>
    {
        IApiAwareEntityBuilder WithAction<TController, TBody>(
            string name,
            Expression<Action<TController>> resource,
            Action<IActionBuilder<TBody>> configure
        ) where TController : class
          where TBody : class;

        IApiAwareEntityBuilder WithAction<TController>(
            string name,
            Expression<Action<TController>> resource
        ) where TController : class;

        IApiAwareEntityBuilder WithActions<TController, TBody>(
            params (
                string name,
                Expression<Action<TController>> resource,
                Action<IActionBuilder<TBody>> configure
            )[] actions
        ) where TController : class
          where TBody : class;

        IApiAwareEntityBuilder WithLink<TController>(
            string name,
            Expression<Action<TController>> resource,
            string[] rel = null
        ) where TController : class;

        IApiAwareEntityBuilder WithLinks<TController>(
            params (
                string name,
                Expression<Action<TController>> resource,
                string[] rel
            )[] links
        ) where TController : class;

        IApiAwareEntityBuilder WithEmbeddedEntity(
            Action<IApiAwareEntityBuilder> newEntity
        );

        IApiAwareEntityBuilder WithEmbeddedEntities(
            params Action<IApiAwareEntityBuilder>[] newEntities
        );

        IApiAwareEntityBuilder WithEmbeddedEntitiesForEach<TOne>(
            IEnumerable<TOne> each,
            Action<IApiAwareEntityBuilder, TOne> newEntityForOne
        );

        IApiAwareEntityBuilder WithLinkedEntity<TController>(
            Expression<Action<TController>> resource,
            string[] classes = null
        ) where TController : class;

        IApiAwareEntityBuilder WithLinkedEntities<TController>(
            params (
                Expression<Action<TController>> resource,
                string[] classes
            )[] entities
        ) where TController : class;

        IApiAwareEntityBuilder WithLinkedEntities<TController>(
            params Expression<Action<TController>>[] resources
        ) where TController : class;

        IApiAwareEntityBuilder WithLinkedEntitiesForEach<TController, TOne>(
            IEnumerable<TOne> each,
            Func<TOne, (Expression<Action<TController>> resource, string[] classes)> linkedEntityForOne
        ) where TController : class;

        IApiAwareEntityBuilder WithLinkedEntitiesForEach<TController, TOne>(
            IEnumerable<TOne> each,
            Func<TOne, Expression<Action<TController>>> linkedEntityForOne
        ) where TController : class;

        IApiAwareEntityBuilder WithProperties<TProps>(
            object properties
        );

        IApiAwareEntityBuilder WithProperties(
            object properties
        );

        IApiAwareEntityBuilder WithClasses(
            params string[] classes
        );
    }
}