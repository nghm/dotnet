namespace Hypermedia.AspNetCore.Siren.Environments
{
    using System.Collections.Generic;

    internal interface IStorage<TStored> : ICollection<TStored>
    {
    }
}