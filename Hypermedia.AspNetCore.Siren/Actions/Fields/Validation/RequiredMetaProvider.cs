namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Validation
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    class RequiredMetaProvider : IValidationMetaProvider
    {
        public IEnumerable<KeyValuePair<string, object>> GetMetadata(object attribute)
        {
            if (attribute is null)
            {
                throw new ArgumentNullException(nameof(attribute));
            }

            if (!(attribute is RequiredAttribute))
            {
                yield break;
            }

            yield return KeyValuePair.Create("required", (object)true);
        }
    }
}
