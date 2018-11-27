using System.Diagnostics.CodeAnalysis;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;

    internal class TypeCodeExtractor : ITypeCodeExtractor
    {
        [ExcludeFromCodeCoverage]
        public TypeCode GetTypeCode(Type fieldDescriptorPropertyType) => Type.GetTypeCode(fieldDescriptorPropertyType);
    }
}