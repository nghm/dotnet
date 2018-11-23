using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using System.ComponentModel.DataAnnotations;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    internal static class BodyParameterMock
    {
        public static int NumberProperty { get; set; } = 1;

        [DataType(DataType.Password)]
        public static string PasswordProperty { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        public static string EmailProperty { get; set; } = string.Empty;

        [DataType(DataType.Url)]
        public static string UrlProperty { get; set; } = string.Empty;

        [DataType(DataType.PhoneNumber)]
        public static string PhoneProperty { get; set; } = string.Empty;

        [DataType(DataType.Text)]
        public static string TextProperty { get; set; } = string.Empty;

        public static string StringProperty { get; set; } = string.Empty;

        //public static FieldDescriptor GetFieldDescriptorOf(IFixture fixture, string propertyName)
        //{
        //    var property = typeof(BodyParameterMock).GetProperty(propertyName);

        //    return new FieldDescriptor(
        //        property.Name,
        //        property.GetValue(BodyParameterMock.),
        //        TestBodyParameter.MatchingTypeProperty.GetType(),
        //        TestBodyParameter.MatchingTypeProperty.GetType().GetCustomAttributes(true));
        //}
    }
}