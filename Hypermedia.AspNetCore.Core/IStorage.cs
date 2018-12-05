namespace Hypermedia.AspNetCore.Store
{
    using System.Collections.Generic;

    public interface IStorage<TStored> : ICollection<TStored>
    {
    }
}