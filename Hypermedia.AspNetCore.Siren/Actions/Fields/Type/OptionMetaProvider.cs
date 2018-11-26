using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.Collections.Generic;

    internal class OptionMetaProvider : ITypeMetaProvider
    {
        private readonly IEnumOptionsExtractor _enumOptionsExtractor;

        public OptionMetaProvider(IEnumOptionsExtractor enumOptionsExtractor)
        {
            this._enumOptionsExtractor =
                enumOptionsExtractor ??
                throw new ArgumentNullException(nameof(enumOptionsExtractor));
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            if (fieldGenerationContext == null)
            {
                throw new ArgumentNullException(nameof(fieldGenerationContext));
            }

            var propertyType = fieldGenerationContext.FieldDescriptor.PropertyType;

            if (!this._enumOptionsExtractor.TryGetEnumOptions(propertyType, out var options))
            {
                yield break;
            }

            yield return KeyValuePair.Create("type", "option" as object);
            yield return KeyValuePair.Create("options", options as object);
        }
    }
}