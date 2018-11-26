using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    internal interface IEnumUtilities
    {
        string[] GetNames(System.Type propertyType);

        Array GetValues(System.Type propertyType);

        bool IsEnum(System.Type propertyType);
    }
}