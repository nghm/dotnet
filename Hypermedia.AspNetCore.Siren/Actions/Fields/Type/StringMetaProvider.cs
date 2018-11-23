using System.Collections.Generic;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.ComponentModel.DataAnnotations;

    internal class StringMetaProvider : ITypeMetaProvider
    {
        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            var propertyType = fieldGenerationContext.FieldDescriptor.PropertyType;

            if (typeof(string) != propertyType)
            {
                yield break;
            }

            var type = "text";

            var dataType = fieldGenerationContext.FieldDescriptor.CustomAttributes
                .OfType<DataTypeAttribute>()
                .SingleOrDefault();

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