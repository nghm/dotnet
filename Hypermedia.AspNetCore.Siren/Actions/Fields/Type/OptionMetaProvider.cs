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

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldDescriptor fieldDescriptor)
        {
            if (fieldDescriptor == null)
            {
                throw new ArgumentNullException(nameof(fieldDescriptor));
            }

            var propertyType = fieldDescriptor.PropertyType;

            if (!this._enumOptionsExtractor.TryGetEnumOptions(propertyType, out var options))
            {
                yield break;
            }

            yield return KeyValuePair.Create("type", "option" as object);
            yield return KeyValuePair.Create("options", options as object);
        }
    }
}