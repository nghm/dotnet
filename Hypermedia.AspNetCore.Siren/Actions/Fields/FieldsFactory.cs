namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Actions;
    using Actions.Fields;
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Linq;
    using Util;

    internal class FieldsFactory : IFieldsFactory
    {
        private readonly IFieldMetadataProviderCollection _fieldMetadataProviderCollection;

        public FieldsFactory(IFieldMetadataProviderCollection fieldMetadataProviderCollection)
        {
            this._fieldMetadataProviderCollection = fieldMetadataProviderCollection;
        }

        public IEnumerable<IField> MakeFields(KeyValuePair<ControllerParameterDescriptor, object> bodyArgument)
        {
            var bodyArgumentValue = bodyArgument.Value.AsPropertyEnumerable(true);

            foreach (var kvp in bodyArgumentValue)
            {
                yield return ComputeField(kvp.Key, kvp.Value, bodyArgument.Key);
            }
        }

        private IField ComputeField(string fieldName, object fieldValue, ParameterDescriptor bodyParameterDescriptor)
        {
            var property = bodyParameterDescriptor.ParameterType.GetProperty(fieldName);

            if (property == null)
            {
                return null;
            }

            var fieldGenerationContext = new FieldGenerationContext(fieldValue, property);

            var metadata = this._fieldMetadataProviderCollection
                .GetMetadataProviders()
                .SelectMany(metaProvider => metaProvider.GetMetadata(fieldGenerationContext));

            return new Field(fieldName, fieldValue, metadata);
        }
    }
}