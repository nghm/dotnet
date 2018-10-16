using System;
using System.Collections.Generic;
using System.Text;

namespace Hypermedia.AspNetCore.Siren.Entities
{
    public interface ITypedEntityBuilder<T> where T : class
    {
        ITypedEntityBuilder<T> AddLink(Action<T> select, params string[] rel);
        ITypedEntityBuilder<T> AddAction(Action<T> select, string name);
        ITypedEntityBuilder<T> AddEntity(Action<T> select, params string[] classes);
        ITypedEntityBuilder<T> AddEntity(Action<IEntityBuilder> select);
        ITypedEntityBuilder<T> AddEntities<M>(IEnumerable<M> enumerable, Action<T, M> configureOne, params string[] classes);
        ITypedEntityBuilder<T> AddEntities<M>(IEnumerable<M> enumerable, Action<IEntityBuilder, M> configureOne);
    }
}
