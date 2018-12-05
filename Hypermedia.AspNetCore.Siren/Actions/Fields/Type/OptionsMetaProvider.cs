namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class OptionsMetaProvider : ITypeMetaProvider
    {
        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldDescriptor fieldDescriptor)
        {
            var propertyType = fieldDescriptor.PropertyType;

            if (!propertyType.IsEnum)
            {
                yield break;
            }

            var options = GetOptions(propertyType);

            yield return KeyValuePair.Create("type", "options" as object);
            yield return KeyValuePair.Create("options", options);
            yield return KeyValuePair.Create("value", fieldDescriptor.Value);
        }

        private static object GetOptions(Type propertyType)
        {
            var names = Enum.GetNames(propertyType).Cast<string>();
            var values = Enum.GetValues(propertyType).Cast<int>().ToArray();

            object options = names.Select((name, index) => new FieldOption { Name = name, Value = values[index] }).ToArray();
            return options;
        }
    }
}