namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;

    internal class TypeCodeExtractor : ITypeCodeExtractor
    {
        public TypeCode GetTypeCode(Type fieldDescriptorPropertyType)
        {
            return Type.GetTypeCode(fieldDescriptorPropertyType);
        }
    }
}