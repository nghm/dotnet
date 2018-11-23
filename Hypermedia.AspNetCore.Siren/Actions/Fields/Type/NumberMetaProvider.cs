namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;
    using System.Collections.Generic;
    using Util;

    internal class NumberMetaProvider : ITypeMetaProvider
    {
        private readonly ISet<TypeCode> _numberTypeCodes = new HashSet<TypeCode>
        {
            TypeCode.Byte,
            TypeCode.SByte,
            TypeCode.UInt16,
            TypeCode.UInt32,
            TypeCode.UInt64,
            TypeCode.Int16,
            TypeCode.Int32,
            TypeCode.Int64,
            TypeCode.Decimal,
            TypeCode.Double,
            TypeCode.Single
        };

        private readonly ITypeCodeExtractor _typeCodeExtractor;

        public NumberMetaProvider(ITypeCodeExtractor typeCodeExtractor)
        {
            Guard.EnsureIsNotNull(typeCodeExtractor, nameof(typeCodeExtractor));

            this._typeCodeExtractor = typeCodeExtractor;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            Guard.EnsureIsNotNull(fieldGenerationContext, nameof(fieldGenerationContext));

            var fieldDescriptorPropertyType = fieldGenerationContext.FieldDescriptor.PropertyType;
            var typeCode = this._typeCodeExtractor.GetTypeCode(fieldDescriptorPropertyType);
            
            if (this._numberTypeCodes.Contains(typeCode))
            {
                yield return KeyValuePair.Create("type", "number" as object);
            }
        }
    }
}