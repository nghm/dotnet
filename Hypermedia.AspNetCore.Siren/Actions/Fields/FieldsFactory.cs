using Hypermedia.AspNetCore.Siren.Endpoints;
using Hypermedia.AspNetCore.Siren.Util;
using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldsFactory : IFieldsFactory
    {
        #region Fields

        private readonly IFieldMetadataProviderCollection _fieldMetadataProviderCollection;

        #endregion Fields

        #region Constructors

        public FieldsFactory(IFieldMetadataProviderCollection fieldMetadataProviderCollection)
        {
            ParameterUtils.NullCheck(fieldMetadataProviderCollection, nameof(fieldMetadataProviderCollection));

            this._fieldMetadataProviderCollection = fieldMetadataProviderCollection;
        }

        #endregion Constructors

        #region Public functions

        public IEnumerable<IField> MakeFields(ActionArgument bodyArgument)
        {
            var argumentFields = bodyArgument.GetFieldDescriptors();

            foreach (var field in argumentFields)
            {
                yield return MakeField(field);
            }
        }

        #endregion Public functions

        #region Private functions

        private IField MakeField(FieldDescriptor fieldDescriptor)
        {
            var fieldGenerationContext = new FieldGenerationContext(fieldDescriptor);

            var metadata = this._fieldMetadataProviderCollection
                .GetMetadataProviders()
                .SelectMany(metaProvider => metaProvider.GetMetadata(fieldGenerationContext));

            return new Field(fieldDescriptor.Name, fieldDescriptor.Value, metadata);
        }

        #endregion Private functions
    }
}