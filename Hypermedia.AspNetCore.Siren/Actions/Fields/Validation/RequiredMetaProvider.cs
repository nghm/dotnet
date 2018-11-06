namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    class RequiredMetaProvider : IValidationMetaProvider
    {
        public bool CanProvideMetadata(object attribute)
        {
            return attribute is RequiredAttribute;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext, object attribute)
        {
            if (!(attribute is RequiredAttribute))
            {
                throw new InvalidCastException();
            }

            yield return KeyValuePair.Create("required", (object) true);
        }
    }
}
