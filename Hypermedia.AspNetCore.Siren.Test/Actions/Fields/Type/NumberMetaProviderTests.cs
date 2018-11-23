using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
using Hypermedia.AspNetCore.Siren.Test.Utils;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    public class NumberMetaProviderTests
    {
        public static FieldDescriptor GetMatchingTypeFieldDescriptor(IFixture fixture)
        {
            return new FieldDescriptor(
                nameof(TestBodyParameter.MatchingTypeProperty),
                TestBodyParameter.MatchingTypeProperty,
                TestBodyParameter.MatchingTypeProperty.GetType(),
                TestBodyParameter.MatchingTypeProperty.GetType().GetCustomAttributes(true));
        }

        public static FieldDescriptor GetNotMatchingTypeFieldDescriptor(IFixture fixture)
        {
            return new FieldDescriptor(
                nameof(TestBodyParameter.NotMatchingTypeProperty),
                TestBodyParameter.NotMatchingTypeProperty,
                TestBodyParameter.NotMatchingTypeProperty.GetType(),
                TestBodyParameter.NotMatchingTypeProperty.GetType().GetCustomAttributes(true));
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata(
            [MockCtorParams(nameof(GetMatchingTypeFieldDescriptor), StaticIndexes = new[] { 1 })]
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
            [MockCtorParams(nameof(GetNotMatchingTypeFieldDescriptor), StaticIndexes = new[] { 1 })]
            FieldGenerationContext fieldGenerationContext,
            NumberMetaProvider numberMetaProvider)
        {
            var meta = numberMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Empty(meta);
        }
    }
}