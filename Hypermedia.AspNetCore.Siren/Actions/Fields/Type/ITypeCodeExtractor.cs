namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;

    internal interface ITypeCodeExtractor
    {
        TypeCode GetTypeCode(Type fieldDescriptorPropertyType);
    }
}