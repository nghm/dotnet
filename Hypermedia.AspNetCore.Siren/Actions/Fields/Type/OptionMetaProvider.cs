namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.Collections.Generic;
    using Util;

    internal class OptionMetaProvider : ITypeMetaProvider
    {
        private readonly IEnumOptionsExtractor _enumOptionsExtractor;

        public OptionMetaProvider(IEnumOptionsExtractor enumOptionsExtractor)
        {
            Guard.EnsureIsNotNull(enumOptionsExtractor, nameof(enumOptionsExtractor));

            this._enumOptionsExtractor = enumOptionsExtractor;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            Guard.EnsureIsNotNull(fieldGenerationContext, nameof(fieldGenerationContext));

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