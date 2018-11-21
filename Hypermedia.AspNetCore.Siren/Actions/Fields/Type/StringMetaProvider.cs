using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.ComponentModel.DataAnnotations;
    using System.Reflection;

    internal class StringMetaProvider : ITypeMetaProvider
    {
        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            var propertyInfo = fieldGenerationContext.PropertyInfo;
            var propertyType = propertyInfo.PropertyType;

            if (typeof(string) != propertyType)
            {
                yield break;
            }

            var type = "text";

            var dataType = propertyInfo.GetCustomAttribute<DataTypeAttribute>();

            if (dataType == null)
            {
                yield return KeyValuePair.Create("type", "text" as object);
                yield break;
            }

            switch (dataType.DataType)
            {
                case DataType.Password:
                    type = "password";
                    break;

                case DataType.EmailAddress:
                    type = "email";
                    break;

                case DataType.Url:
                    type = "url";
                    break;

                case DataType.PhoneNumber:
                    type = "phone";
                    break;
            }

            yield return KeyValuePair.Create("type", (object)type);
        }
    }
}