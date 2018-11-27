using System.Diagnostics.CodeAnalysis;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;

    internal class EnumUtilities : IEnumUtilities
    {
        [ExcludeFromCodeCoverage]
        public bool IsEnum(Type propertyType) => propertyType.IsEnum;

        [ExcludeFromCodeCoverage]
        public string[] GetNames(Type propertyType) => Enum.GetNames(propertyType);

        [ExcludeFromCodeCoverage]
        public Array GetValues(Type propertyType) => Enum.GetValues(propertyType);
    }
}