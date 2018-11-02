using System;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    public interface ITypedEntityBuilder<T> where T : class
    {
        ITypedEntityBuilder<T> WithClasses(params string[] classes);
        ITypedEntityBuilder<T> WithProperties(object properties);

        ITypedEntityBuilder<T> WithAction(string name, Action<T> @select);

        ITypedEntityBuilder<T> WithEntity(Action<IEntityBuilder> select);
        ITypedEntityBuilder<T> WithEntity(Action<T> select, params string[] classes);
        ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne, params string[] classes);
        ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne);

        ITypedEntityBuilder<T> WithLink(string name, Action<T> @select, params string[] rel);
        ITypedEntityBuilder<T> WithLinks(string[] rel, Dictionary<string, Action<T>> links);
        ITypedEntityBuilder<T> WithLinks(Dictionary<string, Action<T>> links);
    }
}
