namespace Hypermedia.AspNetCore.Siren.Actions
{
    using System.Collections.Generic;
    using Fields;

    internal interface IFieldMetadataProviderCollection
    {
        IEnumerable<IFieldMetadataProvider> GetMetadataProviders();
    }
}