namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;
    using System.Linq;

    internal class EnumOptionsExtractor : IEnumOptionsExtractor
    {
        private readonly IEnumUtilities _enumUtils;

        public EnumOptionsExtractor(IEnumUtilities enumUtils)
        {
            this._enumUtils =
                enumUtils ??
                throw new ArgumentNullException(nameof(enumUtils));
        }

        public bool TryGetEnumOptions(Type propertyType, out FieldOption[] options)
        {
            if (propertyType == null)
            {
                throw new ArgumentNullException(nameof(propertyType));
            }

            if (!_enumUtils.IsEnum(propertyType))
            {
                options = null;

                return false;
            }

            var names = _enumUtils.GetNames(propertyType);
            var values = _enumUtils.GetValues(propertyType).Cast<int>().ToArray();

            options = names
                .Select((name, index) => new FieldOption { Name = name, Value = values[index] })
                .ToArray();

            return true;
        }
    }
}