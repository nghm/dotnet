using AutoFixture.Xunit2;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Validation
{
    using Hypermedia.AspNetCore.Siren.Actions.Fields.Validation;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Xunit;

    public class PatternMetaProviderTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldReturnEmptyEnumerableMetadata(
            PatternMetaProvider patternMetaProvider,
            RequiredAttribute attribute)
        {
            var meta = patternMetaProvider.GetMetadata(attribute).ToArray();

            Assert.True(meta.Length == 0);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_PatternAttribute(
            PatternMetaProvider patternMetaProvider,
            [Frozen] string pattern,
            RegularExpressionAttribute regExAttribute)
        {
            var meta = patternMetaProvider.GetMetadata(regExAttribute).ToArray();

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "pattern" && (value as string) == pattern;
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldHandleNullAttribute(
            PatternMetaProvider patternMetaProvider)
        {
            Assert.Throws<ArgumentNullException>(() => patternMetaProvider.GetMetadata(null).ToArray());
        }
    }
}