using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
using Hypermedia.AspNetCore.Siren.Test.Utils;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System.Reflection;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    public class NumberMetaProviderTests
    {
        private class TestBodyParameter
        {
            public static int MatchingTypeProperty { get; set; }

            public static string NotMatchingTypeProperty { get; set; }
        }

        public static PropertyInfo GetMatchingTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.MatchingTypeProperty));
        }

        public static PropertyInfo GetNotMatchingTypePropertyInfo(IFixture fixture)
        {
            return typeof(TestBodyParameter).GetProperty(nameof(TestBodyParameter.NotMatchingTypeProperty));
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata(
            [MockCtorParams(1, nameof(GetMatchingTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            NumberMetaProvider numberMetaProvider)
        {
            var meta = numberMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "number";
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnEmptyMetadata_OtherType(
            [MockCtorParams(null, nameof(GetNotMatchingTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            NumberMetaProvider numberMetaProvider)
        {
            var meta = numberMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Empty(meta);
        }
    }
}