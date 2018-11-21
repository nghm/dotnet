using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
using Hypermedia.AspNetCore.Siren.Test.Utils;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    public class StringMetaProviderTests
    {
        private class TestBodyParameter
        {
            [DataType(DataType.Password)]
            public static string PasswordTypeProperty { get; set; }

            [DataType(DataType.EmailAddress)]
            public static string EmailTypeProperty { get; set; }

            [DataType(DataType.Url)]
            public static string UrlTypeProperty { get; set; }

            [DataType(DataType.PhoneNumber)]
            public static string PhoneTypeProperty { get; set; }

            [DataType(DataType.Text)]
            public static string TextTypeProperty { get; set; }

            public static string AttributelessTypeProperty { get; set; }

            public static int NotMatchingTypeProperty { get; set; }
        }

        public static PropertyInfo GetPasswordTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.PasswordTypeProperty));
        }

        public static PropertyInfo GetEmailTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.EmailTypeProperty));
        }

        public static PropertyInfo GetUrlTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.UrlTypeProperty));
        }

        public static PropertyInfo GetPhoneTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.PhoneTypeProperty));
        }

        public static PropertyInfo GetTextTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.TextTypeProperty));
        }

        public static PropertyInfo GetAttributelessTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.AttributelessTypeProperty));
        }

        public static PropertyInfo GetNotMatchingTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.NotMatchingTypeProperty));
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_PasswordType(
            [MockCtorParams(null, nameof(GetPasswordTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "password";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_EmailType(
            [MockCtorParams(null, nameof(GetEmailTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "email";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_UrlType(
            [MockCtorParams(null, nameof(GetUrlTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "url";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_PhoneNumberType(
            [MockCtorParams(null, nameof(GetPhoneTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "phone";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_TextType(
            [MockCtorParams(null, nameof(GetTextTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "text";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_Attributeless(
            [MockCtorParams(null, nameof(GetAttributelessTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "text";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnEmptyMetadata_OtherType(
            [MockCtorParams(1, nameof(GetNotMatchingTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Empty(meta);
        }
    }
}