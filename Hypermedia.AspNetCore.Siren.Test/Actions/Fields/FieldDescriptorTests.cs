using Hypermedia.AspNetCore.Siren.Actions.Fields;
using System;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Xunit;

    public class FieldDescriptorTests
    {
        [Theory]
        [AutoMockData]
        public void ShouldThrowArgumentNullExceptionWhenNameIsNull(
            object value,
            System.Type propertyType,
            object[] customAttributes)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FieldDescriptor(
                    null,
                    value,
                    propertyType,
                    customAttributes
                    )
            );
        }

        [Theory]
        [InlineAutoMockData("")]
        [InlineAutoMockData(" ")]
        public void ShouldThrowArgumentExceptionWhenNameIsInvalid(
            string name,
            object value,
            System.Type propertyType,
            object[] customAttributes)
        {
            Assert.Throws<ArgumentException>(() =>
                new FieldDescriptor(
                    name,
                    value,
                    propertyType,
                    customAttributes
                )
            );
        }

        [Theory]
        [AutoMockData]
        public void ShouldThrowArgumentNullExceptionWhenTypeIsNull(
            string name,
            object value,
            object[] customAttributes)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FieldDescriptor(
                    name,
                    value,
                    null,
                    customAttributes
                )
            );
        }

        [Theory]
        [AutoMockData]
        public void ShouldThrowArgumentNullExceptionWhenCustomAttributesAreNull(
            string name,
            object value,
            System.Type propertyType)
        {
            Assert.Throws<ArgumentNullException>(() =>
                new FieldDescriptor(
                    name,
                    value,
                    propertyType,
                    null
                )
            );
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstanceWithMinimalNumberOfNonNullValues(
            string name,
            System.Type propertyType,
            object[] customAttributes)
        {
            try
            {
                var _ = new FieldDescriptor(name, null, propertyType, customAttributes);
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
            System.Type propertyType,
            object[] customAttributes)
        {
            try
            {
                var _ = new FieldDescriptor(name, value, propertyType, customAttributes);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }
    }
}