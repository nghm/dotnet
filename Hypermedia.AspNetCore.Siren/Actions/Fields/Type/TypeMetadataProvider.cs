namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    internal class TypeMetadataProvider : IFieldMetadataProvider
    {
        private readonly ITypeMetaProvider[] _typeMetaProviders;

        public TypeMetadataProvider(ITypeMetaProvider[] typeMetaProviders)
        {
            this._typeMetaProviders = typeMetaProviders;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            return this._typeMetaProviders.SelectMany(tmp => tmp.GetMetadata(fieldGenerationContext));
        }
    }
}