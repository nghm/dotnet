using System.ComponentModel.DataAnnotations;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    internal interface IStringPropertyTypeMap
    {
        string MapDataType(DataType dataType);
    }
}