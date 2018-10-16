using System;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    public interface IEntityBuilder
    {
        IEntityBuilder AddClasses(params string[] classes);
        IEntityBuilder AddProperties(object properties);
        IEntityBuilder AddProperties<T>(T properties);

        IEntityBuilder AddAction<T>(Action<T> select, string name) where T : class;

        IEntityBuilder AddEntity<T>(Action<T> select, params string[] classes) where T : class;
        IEntityBuilder AddEntity<T>(Action<IEntityBuilder> builder) where T : class;
        IEntityBuilder AddEntities<M>(IEnumerable<M> enumerable, Action<IEntityBuilder, M> configureOne);
        IEntityBuilder AddEntities<T, M>(IEnumerable<M> enumerable, Action<T, M> configureOne, string[] classes) where T : class;

        IEntityBuilder AddLink<T>(Action<T> select, params string[] rel) where T : class;
        IEntityBuilder AddLinks<T>(string[] rel, params Action<T>[] selectors) where T : class;

        IEntityBuilder AddFrom<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T: class;

        IEntity Build();
    }
}
