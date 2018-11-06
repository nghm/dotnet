namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Collections.Generic;

    internal interface IFieldMetadataProvider
    {
        IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext);
    }
}