namespace Hypermedia.AspNetCore.Siren.Actions
{
    using System.Collections.Generic;
    using Fields;
    using Fields.Type;

    internal class FieldMetadataProviderCollection
    {
        private readonly TypeMetadataProvider _typeMetadataProvider;
        private readonly ValidationMetadataProvider _validationMetaProvider;

        public FieldMetadataProviderCollection(
            TypeMetadataProvider typeMetadataProvider,
            ValidationMetadataProvider validationMetaProvider
        )
        {
            this._typeMetadataProvider = typeMetadataProvider;
            this._validationMetaProvider = validationMetaProvider;
        }

        public IEnumerable<IFieldMetadataProvider> GetMetadataProviders()
        {
            yield return this._typeMetadataProvider;
            yield return this._validationMetaProvider;
        }
    }
}