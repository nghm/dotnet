using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Validation;
using Hypermedia.AspNetCore.Siren.Test.Utils;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Validation
{
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
    string pattern)
        {
            var attribute = new RegularExpressionAttribute(pattern);
            var meta = patternMetaProvider.GetMetadata(attribute).ToArray();

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
