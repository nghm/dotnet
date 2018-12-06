using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Collections.Generic;
    using Validation;

    internal class ValidationMetadataProvider : IFieldMetadataProvider
    {
        private readonly IValidationMetaProvider[] _validationMetaProviders;

        public ValidationMetadataProvider(IValidationMetaProvider[] validationMetaProviders)
        {
            this._validationMetaProviders = 
                validationMetaProviders ??
                throw new ArgumentNullException(nameof(validationMetaProviders));
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldDescriptor fieldDescriptor)
        {
            if (fieldDescriptor == null)
            {
                throw new ArgumentNullException(nameof(fieldDescriptor));
            }

            var attributes = fieldDescriptor.CustomAttributes;
            var results = new List<KeyValuePair<string, object>>();

            foreach (var validationMetaProvider in this._validationMetaProviders)
            {
                foreach (var attribute in attributes)
                {
                    results.AddRange(validationMetaProvider.GetMetadata(attribute));
                }
            }

            return results;
        }
    }
}