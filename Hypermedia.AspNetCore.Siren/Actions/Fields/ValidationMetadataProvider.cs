namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Collections.Generic;
    using Validation;

    internal class ValidationMetadataProvider : IFieldMetadataProvider
    {
        private readonly IValidationMetaProvider[] _validationMetaProviders;

        public ValidationMetadataProvider(IValidationMetaProvider[] validationMetaProviders)
        {
            this._validationMetaProviders = validationMetaProviders;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            var propertyInfo = fieldGenerationContext.PropertyInfo;
            var attributes = propertyInfo.GetCustomAttributes(true);
            var results = new List<KeyValuePair<string, object>>();

            foreach (var validationMetaProvider in this._validationMetaProviders)
            {
                foreach (var attribute in attributes)
                {
                    if (validationMetaProvider.CanProvideMetadata(attribute))
                    {
                        results.AddRange(validationMetaProvider.GetMetadata(fieldGenerationContext, attribute));
                    }
                }
            }

            return results;
        }
    }
}