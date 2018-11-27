namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.ComponentModel.DataAnnotations;

    internal interface IDataTypeAttributeExtractor
    {
        DataTypeAttribute GetDataTypeAttribute(object[] customAttributes);
    }
}