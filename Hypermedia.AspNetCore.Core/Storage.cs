namespace Hypermedia.AspNetCore.Core
{
    using System.Collections;
    using System.Collections.Generic;
    using Store;

    internal class Storage<TStored> : IStorage<TStored>
    {
        private ICollection<TStored> _store = new List<TStored>();

        public int Count => _store.Count;

        public bool IsReadOnly => _store.IsReadOnly;

        public void Add(TStored item)
        {
            this._store.Add(item);
        }

        public void Clear()
        {
            this._store.Clear();
        }

        public bool Contains(TStored item)
        {
            return this._store.Contains(item);
        }

        public void CopyTo(TStored[] array, int arrayIndex)
        {
            this._store.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TStored> GetEnumerator()
        {
            return this._store.GetEnumerator();
        }

        public bool Remove(TStored item)
        {
            return this._store.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._store.GetEnumerator();
        }
    }
}
