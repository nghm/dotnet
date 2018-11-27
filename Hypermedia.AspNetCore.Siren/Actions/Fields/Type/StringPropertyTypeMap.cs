namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.ComponentModel.DataAnnotations;

    internal class StringPropertyTypeMap : IStringPropertyTypeMap
    {
        public string MapDataType(DataType dataType)
        {
            var type = "text";

            switch (dataType)
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

            return type;
        }
    }
}