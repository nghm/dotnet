namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Validation
{
    using System.Collections.Generic;

    internal interface IValidationMetaProvider
    {
        IEnumerable<KeyValuePair<string, object>> GetMetadata(object attribute);
    }
}