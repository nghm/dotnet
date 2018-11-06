namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Validation
{
    using System.Collections.Generic;

    internal interface IValidationMetaProvider
    {
        bool CanProvideMetadata(object attribute);

        IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext, object attribute);
    }
}