namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    class PatternMetaProvider : IValidationMetaProvider
    {
        public IEnumerable<KeyValuePair<string, object>> GetMetadata(object attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (!(attribute is RegularExpressionAttribute regularExpressionAttribute))
            {
                yield break;
            }

            yield return KeyValuePair.Create("pattern", (object) regularExpressionAttribute.Pattern);
        }
    }
}
