using Hypermedia.AspNetCore.AsyncStepBuilder;

namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Hypermedia.AspNetCore.Siren.Actions;
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IResourceBuilder : IAsyncBuilder<IEntity>
    {
        IResourceBuilder WithAction<TController, TBody>(
            string name,
            Expression<Action<TController>> resource,
            Action<IActionBuilder<TBody>> configure
        ) where TController : class
          where TBody : class;

        IResourceBuilder WithAction<TController>(
            string name,
            Expression<Action<TController>> resource
        ) where TController : class;

        IResourceBuilder WithActions<TController, TBody>(
            params (
                string name,
                Expression<Action<TController>> resource,
                Action<IActionBuilder<TBody>> configure
            )[] actions
        ) where TController : class
          where TBody : class;

        IResourceBuilder WithLink<TController>(
            string name,
            Expression<Action<TController>> resource,
            string[] rel = null
        ) where TController : class;

        IResourceBuilder WithLinks<TController>(
            params (
                string name,
                Expression<Action<TController>> resource,
                string[] rel
            )[] links
        ) where TController : class;

        IResourceBuilder WithEmbeddedEntity(
            Action<IResourceBuilder> newEntity
        );

        IResourceBuilder WithEmbeddedEntities(
            params Action<IResourceBuilder>[] newEntities
        );

        IResourceBuilder WithEmbeddedEntitiesForEach<TOne>(
            IEnumerable<TOne> each,
            Action<IResourceBuilder, TOne> newEntityForOne
        );

        IResourceBuilder WithLinkedEntity<TController>(
            Expression<Action<TController>> resource,
            string[] classes = null
        ) where TController : class;

        IResourceBuilder WithLinkedEntities<TController>(
            params (
                Expression<Action<TController>> resource,
                string[] classes
            )[] entities
        ) where TController : class;

        IResourceBuilder WithLinkedEntities<TController>(
            params Expression<Action<TController>>[] resources
        ) where TController : class;

        IResourceBuilder WithLinkedEntitiesForEach<TController, TOne>(
            IEnumerable<TOne> each,
            Func<TOne, (Expression<Action<TController>> resource, string[] classes)> linkedEntityForOne
        ) where TController : class;

        IResourceBuilder WithLinkedEntitiesForEach<TController, TOne>(
            IEnumerable<TOne> each,
            Func<TOne, Expression<Action<TController>>> linkedEntityForOne
        ) where TController : class;

        IResourceBuilder WithProperties<TProps>(
            object properties
        );

        IResourceBuilder WithProperties(
            object properties
        );

        IResourceBuilder WithClasses(
            params string[] classes
        );
    }
}