using Hypermedia.AspNetCore.Siren.Actions.Fields;
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
            try
            {
                var _ = new Field(name, null);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            string name,
            object value,
            ICollection<KeyValuePair<string, object>> metadata)
        {
            try
            {
                var _ = new Field(name, value, metadata);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenMetadataKeyIsNull(
            object value,
            Field field)
        {
            var meta = new KeyValuePair<string, object>(null, value);

            Assert.Throws<ArgumentNullException>(() => field.AddMetadata(meta));
        }

        [Theory]
        [InlineAutoMockData("")]
        [InlineAutoMockData("  ")]
        private void ShouldThrowArgumentExceptionWhenMetadataKeyIsInvalid(
            string key,
            object value,
            Field field)
        {
            var meta = new KeyValuePair<string, object>(key, value);

            Assert.Throws<ArgumentException>(() => field.AddMetadata(meta));
        }

        [Theory]
        [AutoMockData]
        private void ShouldAddMetaUsingAddMethod(
            KeyValuePair<string, object> newMeta,
            string name,
            object value,
            Mock<ICollection<KeyValuePair<string, object>>> metadata)
        {
            var field = new Field(name, value, metadata.Object);

            field.AddMetadata(newMeta);

            metadata.Verify(m => m.Add(newMeta), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldAddMeta(
            KeyValuePair<string, object> newMeta,
            Field field
            )
        {
        }
    }
}