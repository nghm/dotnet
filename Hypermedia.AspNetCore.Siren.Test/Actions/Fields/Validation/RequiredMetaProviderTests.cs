using AutoFixture;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Siren.Actions.Fields.Validation;
using Hypermedia.AspNetCore.Siren.Test.Utils;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Validation
{
    public class RequiredMetaProviderTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldReturnEmptyEnumerableMetadata(
RequiredMetaProvider patternMetaProvider,
RegularExpressionAttribute attribute)
        {
            var meta = patternMetaProvider.GetMetadata(attribute).ToArray();

            Assert.True(meta.Length == 0);
        }


        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadata_RequiredAttribute(
    RequiredMetaProvider patternMetaProvider,
    RequiredAttribute attribute)
        {
            var meta = patternMetaProvider.GetMetadata(attribute).ToArray();

            Assert.Contains(meta, mp =>
            {
                var (key, value) = mp;

                return key == "required" && (value as bool?) == true;
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldHandleNullAttribute(
    RequiredMetaProvider patternMetaProvider)
        {
            Assert.Throws<ArgumentNullException>(() => patternMetaProvider.GetMetadata(null).ToArray());
        }
    }
}
