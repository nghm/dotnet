namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;
    using System.Linq;

    internal class EnumOptionsExtractor : IEnumOptionsExtractor
    {
        public bool TryGetEnumOptions(Type propertyType, out FieldOption[] options)
        {
            if (!propertyType.IsEnum)
            {
                options = null;

                return false;
            }

            var names = Enum.GetNames(propertyType).Cast<string>();
            var values = Enum.GetValues(propertyType).Cast<int>().ToArray();

            options = names
                .Select((name, index) => new FieldOption { Name = name, Value = values[index] })
                .ToArray();

            return true;
        }
    }
}