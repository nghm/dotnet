using System;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    public interface IEntityBuilder
    {
        IEntityBuilder WithClasses(params string[] classes);
        IEntityBuilder WithProperties(object properties);
        IEntityBuilder WithProperties<T>(T properties);

        IEntityBuilder WithAction<T>(string name, Action<T> @select) where T : class;

        IEntityBuilder WithEntity<T>(Action<T> select, params string[] classes) where T : class;
        IEntityBuilder WithEntity<T>(Action<IEntityBuilder> builder) where T : class;
        IEntityBuilder WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne);
        IEntityBuilder WithEntities<T, TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne, string[] classes) where T : class;

        IEntityBuilder WithLink<T>(string name, Action<T> @select, params string[] rel) where T : class;
        IEntityBuilder WithLinks<T>(string[] rel, IDictionary<string, Action<T>> links) where T : class;
        IEntityBuilder WithLinks<T>(IDictionary<string, Action<T>> links) where T : class;

        IEntityBuilder With<T>(Action<ITypedEntityBuilder<T>> entityBuilderConfiguration) where T: class;

        IEntity Build();
    }
}
