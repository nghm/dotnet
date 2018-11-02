using System;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.WebApi.Services
{
    public class MockCrudService<T> where T: class
    {
        private readonly IDictionary<int, T> _stored;
        private readonly Action<int, T> _setIndex;
        private readonly Func<T, int> _getIndex;

        public MockCrudService(
            Func<int, T> makeOne, 
            Func<T, int> getIndex, 
            Action<int, T> setIndex)
        {
            this._stored = Infinite()
                .Take(40)
                .Select(makeOne)
                .ToDictionary(getIndex, kvp => kvp);
            this._getIndex = getIndex;
            this._setIndex = setIndex;
        }

        internal IQueryable<T> All() =>
            this._stored
                .Select(kvp => kvp.Value)
                .AsQueryable();

        internal void Update(int id, T entity)
        {
            if (!this._stored.ContainsKey(id))
            {
                throw new InvalidOperationException("The entity was not found");
            }

            this._stored[id] = entity;
        }

        internal void Create(T entity)
        {
            var id = this._stored.Max(kvp => this._getIndex(kvp.Value)) + 1;

            this._setIndex(id, entity) ;

            this._stored[this._getIndex(entity)] = entity;
        }

        internal T One(int id)
        {
            this._stored.TryGetValue(id, out var returnedValue);

            return returnedValue;
        }

        static IEnumerable<int> Infinite(int value = 0)
        {
            do
            {
                yield return value++;
            } while (value > 0);
        }

        public int Count()
        {
            return this._stored.Count;
        }
    }
}
