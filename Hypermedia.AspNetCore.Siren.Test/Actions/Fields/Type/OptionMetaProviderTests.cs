using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
using Hypermedia.AspNetCore.Siren.Test.Utils;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    public class OptionMetaProviderTests
    {
        private enum TestEnum
        {
            None = 0,
            Something = 1
        }

        private class TestBodyParameter
        {
            public static TestEnum MatchingTypeProperty { get; set; }

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
        private void ShouldReturnMetadataWithAllOptions(
            [MockCtorParams(TestEnum.None, nameof(GetMatchingTypePropertyInfo), StaticIndexes = new[] { 2 })]
            FieldGenerationContext fieldGenerationContext,
            OptionMetaProvider optionMetaProvider)
        {
            var meta = optionMetaProvider.GetMetadata(fieldGenerationContext);

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "type" && (value as string) == "option";
            });

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "options"
                       && ((value as FieldOption[]) ?? throw new InvalidOperationException())
                           .All(fo => Enum.GetName(typeof(TestEnum), fo.Value) == fo.Name);
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