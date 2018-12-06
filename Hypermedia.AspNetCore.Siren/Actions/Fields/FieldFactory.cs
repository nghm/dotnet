namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Builders.Abstractions;
    using System;
    using System.Linq;

    internal class FieldFactory : IFieldFactory
    {
        private readonly IFieldMetadataProviderCollection _fieldMetadataProviderCollection;

        public FieldFactory(IFieldMetadataProviderCollection fieldMetadataProviderCollection)
        {
            this._fieldMetadataProviderCollection =
                fieldMetadataProviderCollection ??
                throw new ArgumentNullException(nameof(fieldMetadataProviderCollection));
        }

        public IField MakeField(FieldDescriptor fieldDescriptor)
        {
            if (fieldDescriptor == null)
            {
                throw new ArgumentNullException(nameof(fieldDescriptor));
            }

            var metadata = this._fieldMetadataProviderCollection
                .GetMetadataProviders()
                .SelectMany(metaProvider => metaProvider.GetMetadata(fieldDescriptor));

            return new Field(fieldDescriptor.Name, fieldDescriptor.Value, metadata.ToList());
        }
    }
}