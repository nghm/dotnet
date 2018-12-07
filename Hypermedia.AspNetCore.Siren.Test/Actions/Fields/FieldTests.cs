using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Tests.Common;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Collections.Generic;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class FieldTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenNameIsNull(
            object value,
            ICollection<KeyValuePair<string, object>> metadata)
        {
            Assert.Throws<ArgumentNullException>(() => new Field(null, value, metadata));
        }

        [Theory]
        [InlineAutoMockData("")]
        [InlineAutoMockData("  ")]
        private void ShouldThrowArgumentExceptionWhenNameIsInvalid(
            string name,
            object value,
            ICollection<KeyValuePair<string, object>> metadata)
        {
            Assert.Throws<ArgumentException>(() => new Field(name, value, metadata));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstanceWithMinimalNumberOfNonNullArguments(
            string name)
        {
            AssertUtils.NoExceptions(() => new Field(name, null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            string name,
            object value,
            ICollection<KeyValuePair<string, object>> metadata)
        {
            AssertUtils.NoExceptions(() => new Field(name, value, metadata));
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetMetadata(
            [Frozen] ICollection<KeyValuePair<string, object>> expectedResult,
            Field sut)
        {
            var metadata = sut.GetMetadata();

            Assert.Equal(expectedResult, metadata);
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenMetadataKeyIsNull(
            object value,
            Field sut)
        {
            var meta = new KeyValuePair<string, object>(null, value);

            Assert.Throws<ArgumentNullException>(() => sut.AddMetadata(meta));
        }

        [Theory]
        [InlineAutoMockData("")]
        [InlineAutoMockData("  ")]
        private void ShouldThrowArgumentExceptionWhenMetadataKeyIsInvalid(
            string key,
            object value,
            Field sut)
        {
            var meta = new KeyValuePair<string, object>(key, value);

            Assert.Throws<ArgumentException>(() => sut.AddMetadata(meta));
        }

        //TODO: wait for storage implementation
        [Theory]
        [AutoMockData]
        private void ShouldAddMetaUsingAddMethod(
            KeyValuePair<string, object> newMeta,
            [Frozen]Mock<ICollection<KeyValuePair<string, object>>> metadata,
            Field sut)
        {
            sut.AddMetadata(newMeta);

            metadata.Verify(m => m.Add(newMeta), Times.Once);
        }
    }
}