﻿namespace Hypermedia.AspNetCore.Siren.Actions.Fields
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
            var attributes = fieldGenerationContext.FieldDescriptor.CustomAttributes;
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