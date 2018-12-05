using System;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.Collections.Generic;

    internal class TypeMetadataProvider : IFieldMetadataProvider
    {
        private readonly ITypeMetaProvider[] _typeMetaProviders;

        public TypeMetadataProvider(IEnumerable<ITypeMetaProvider> typeMetaProviders)
        {
            if (typeMetaProviders == null)
            {
                throw new ArgumentNullException(nameof(typeMetaProviders));
            }

            this._typeMetaProviders = typeMetaProviders.ToArray();
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldDescriptor fieldDescriptor)
        {
            if (fieldDescriptor == null)
            {
                throw new ArgumentNullException(nameof(fieldDescriptor));
            }

            foreach (var typeMetaProvider in this._typeMetaProviders)
            {
                foreach (var keyValuePair in typeMetaProvider.GetMetadata(fieldDescriptor))
                {
                    yield return keyValuePair;
                }
            }
        }
    }
}