namespace Books.Infrastructure.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InMemoryCrudService<T, TKey> : ICrudService<T, TKey> 
        where T: class, IEntity<TKey>
    {
        private readonly IGuidGenerator<TKey> _guidGenerator;
        private readonly IDictionary<TKey, T> _stored = new Dictionary<TKey, T>();

        public IQueryable<T> All() =>
            this._stored
                .Select(kvp => kvp.Value).AsQueryable<T>();

        public InMemoryCrudService(IGuidGenerator<TKey> guidGenerator)
        {
            this._guidGenerator = guidGenerator;
        }

        public void Update(TKey id, T entity)
        {
            if (!this._stored.ContainsKey(id))
            {
                throw new InvalidOperationException("The entity was not found");
            }

            this._stored[id] = entity;
        }

        public void Create(T entity)
        {
            entity.Id = this._guidGenerator.Make();

            this._stored[entity.Id] = entity;
        }

        public T One(TKey id)
        {
            this._stored.TryGetValue(id, out var returnedValue);

            return returnedValue;
        }

        public int Count()
        {
            return this._stored.Count;
        }
    }

    public interface IGuidGenerator<out TKey>
    {
        TKey Make();
    }

    public interface IEntity<TKey>
    {
        TKey Id { get; set; }
    }
}
