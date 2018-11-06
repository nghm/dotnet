namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;

    internal class TypeMetadata : IFieldMetadata
    {
        private readonly FieldGenerationContext _fieldGenerationContext;

        public TypeMetadata(FieldGenerationContext fieldGenerationContext)
        {
            this._fieldGenerationContext = fieldGenerationContext;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata()
        {
            var propertyInfo = this._fieldGenerationContext
                .ParameterInfo
                .ParameterType
                .GetProperty(this._fieldGenerationContext.Name);
            var propertyType = propertyInfo
                .PropertyType;

            var unguarded = GetMetadataNotNullGuarded(propertyInfo, propertyType);
            
            foreach(var kvp in unguarded)
            {
                if (this._fieldGenerationContext.Value == null && kvp.Key == "type")
                {
                    var type = kvp.Value.ToString();
                    
                    switch(type)
                    {
                        case "password":
                        case "url":
                        case "email":
                        case "text":
                            yield return KeyValuePair.Create("value", "" as object); break;
                        case "date":
                            yield return KeyValuePair.Create("value", DateTime.Now as object); break;
                        case "number":
                            yield return KeyValuePair.Create("value", 0 as object); break;
                        case "option":
                            yield return KeyValuePair.Create("value", Enum.GetValues(propertyType).GetValue(0)); break;
                        case "options":
                            yield return KeyValuePair.Create("value", new object[] { } as object); break;
                    }
                }

                yield return kvp;
            }
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadataNotNullGuarded(PropertyInfo propertyInfo, Type propertyType)
        {
            var type = "";

            if (propertyType.IsEnum)
            {
                yield return KeyValuePair.Create("type", "option" as object);
                var names = Enum.GetNames(propertyType).Cast<string>();
                var values = Enum.GetValues(propertyType).Cast<int>().ToArray();
                object options = names.Select((name, index) => new FieldOption { Name = name, Value = values[index] }).ToArray();

                yield return KeyValuePair.Create("options", options);
                yield break;
            }

            switch (Type.GetTypeCode(propertyType))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single: type = "number"; break;
                case TypeCode.DateTime: type = "date"; break;
                case TypeCode.Boolean: type = "boolean"; break;
            }

            if (!string.IsNullOrEmpty(type))
            {
                yield return KeyValuePair.Create("type", type as object);
                yield break;
            }

            if (typeof(string) == propertyType)
            {
                var dataType = propertyInfo.GetCustomAttribute<DataTypeAttribute>();

                if (dataType == null)
                {
                    yield return KeyValuePair.Create("type", "text" as object);
                    yield break;
                } 

                switch (dataType.DataType)
                {
                    case DataType.Date:
                    case DataType.DateTime:
                        type = "date";
                        break;

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

                yield return KeyValuePair.Create("type", type as object);
                yield break;
            }

            if (propertyType.IsEnum)
            {
                yield return KeyValuePair.Create("type", "option" as object);
                yield return KeyValuePair.Create("options",
                        Enum.GetNames(propertyType).Join(
                            Enum.GetValues(propertyType) as object[],
                            inner => inner, outer => outer,
                            (name, value) => new {
                                name,
                                value
                            }
                        ) as object
                );
                yield break;
            }

            if (!typeof(IEnumerable<>).IsAssignableFrom(propertyType))
            {
                throw new InvalidOperationException(
                    "Cannot generate type metadata for field " +
                    this._fieldGenerationContext.Name
                );
            }

            yield return KeyValuePair.Create("type", "options" as object);
        }
    }
}