using System;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.WebApi.Services
{
    public class MockCrudService<T> where T: class
    {
        readonly IDictionary<int, T> Stored;
        public Func<T, int> GetIndex { get; }
        public Action<int, T> SetIndex { get; }

        public MockCrudService(
            Func<int, T> makeOne, 
            Func<T, int> getIndex, 
            Action<int, T> setIndex)
        {
            Stored = Infinite()
                .Take(40)
                .Select(makeOne)
                .ToDictionary(kvp => getIndex(kvp), kvp => kvp);
            GetIndex = getIndex;
            SetIndex = setIndex;
        }

        internal IQueryable<T> All() =>
            Stored
                .Select(kvp => kvp.Value)
                .AsQueryable();

        internal void Update(int id, T entity)
        {
            if (!Stored.ContainsKey(id))
            {
                throw new Exception("The entity was not found");
            }

            Stored[id] = entity;
        }
        internal void Create(T entity)
        {
            var id = Stored.Max(kvp => GetIndex(kvp.Value)) + 1;

            SetIndex(id, entity) ;

            Stored[GetIndex(entity)] = entity;
        }

        internal T One(int id)
        {
            return Stored.ContainsKey(id) ? Stored[id] : null as T;
        }

        IEnumerable<int> Infinite()
        {
            int value = 0;
            while (true)
            {
                yield return value++;
            }
        }
    }
}
