namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    class PatternMetaProvider : IValidationMetaProvider
    {
        public bool CanProvideMetadata(object attribute)
        {
            return attribute is RegularExpressionAttribute;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext, object attribute)
        {
            if (!(attribute is RegularExpressionAttribute regularExpressionAttribute))
            {
                throw new InvalidCastException();
            }

            yield return KeyValuePair.Create("pattern", (object) regularExpressionAttribute.Pattern);
        }
    }
}
