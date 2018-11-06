using System;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    using System.Linq.Expressions;

    public interface ITypedEntityBuilder<T> where T : class
    {
        ITypedEntityBuilder<T> WithClasses(params string[] classes);
        ITypedEntityBuilder<T> WithProperties<TProp>(TProp properties);

        ITypedEntityBuilder<T> WithAction(string name, Expression<Action<T>> @select);

        ITypedEntityBuilder<T> WithEntity(Action<IEntityBuilder> select);
        ITypedEntityBuilder<T> WithEntity(Expression<Action<T>> @select, params string[] classes);
        ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<T, TM> configureOne,
            params string[] classes);
        ITypedEntityBuilder<T> WithEntities<TM>(IEnumerable<TM> enumerable, Action<IEntityBuilder, TM> configureOne);

        ITypedEntityBuilder<T> WithLink(string name, Expression<Action<T>> @select, params string[] rel);
        ITypedEntityBuilder<T> WithLinks(string[] rel, Dictionary<string, Expression<Action<T>>> links);
        ITypedEntityBuilder<T> WithLinks(Dictionary<string, Expression<Action<T>>> links);
    }
}
