namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;
    using System.Collections.Generic;

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

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            var typeCode = Type.GetTypeCode(fieldGenerationContext.PropertyInfo.PropertyType);

            if (this._numberTypeCodes.Contains(typeCode))
            {
                yield return KeyValuePair.Create("type", "number" as object);
            }
        }
    }
}