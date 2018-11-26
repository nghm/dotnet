namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;

    internal class EnumUtilities : IEnumUtilities
    {
        public bool IsEnum(Type propertyType) => propertyType.IsEnum;

        public string[] GetNames(Type propertyType) => Enum.GetNames(propertyType);

        public Array GetValues(Type propertyType) => Enum.GetValues(propertyType);
    }
}