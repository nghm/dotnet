using Moq;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    using Hypermedia.AspNetCore.Siren.Actions.Fields;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using Xunit;

    public class ActionArgumentTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenNameIsNull(
            object value,
            object defaultValue,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors
            )
        {
            Assert.Throws<ArgumentNullException>(() => new ActionArgument(null, value, defaultValue, bindingSource, fieldDescriptors));
        }

        [Theory]
        [InlineAutoMockData("")]
        [InlineAutoMockData("  ")]
        private void ShouldThrowArgumentExceptionWhenNameIsInvalid(
            string name,
            object value,
            object defaultValue,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors
        )
        {
            Assert.Throws<ArgumentException>(() => new ActionArgument(name, value, defaultValue, bindingSource, fieldDescriptors));
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenMissingBindingSource(
            string name,
            object value,
            object defaultValue,
            FieldDescriptor[] fieldDescriptors)
        {
            Assert.Throws<ArgumentNullException>(() => new ActionArgument(name, value, defaultValue, null, fieldDescriptors));
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenMissingFieldDescriptors(
            string name,
            object value,
            object defaultValue,
            BindingSource bindingSource)
        {
            Assert.Throws<ArgumentNullException>(() => new ActionArgument(name, value, defaultValue, bindingSource, null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstanceWithMinimalNumberOfNonNullValues(
            string name,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors)
        {
            try
            {
                var _ = new ActionArgument(name, null, null, bindingSource, fieldDescriptors);
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
            object defaultValue,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors)
        {
            try
            {
                var _ = new ActionArgument(name, value, defaultValue, bindingSource, fieldDescriptors);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldCompareNotNullValueUsingEquals(
                Mock<object> value,
                object defaultValue,
                string name,
                BindingSource bindingSource,
                FieldDescriptor[] fieldDescriptors)
        {
            var actionArgument = new ActionArgument(name, value.Object, defaultValue, bindingSource, fieldDescriptors);

            actionArgument.ValueIsDefaultValue();

            value.Verify(v => v.Equals(defaultValue), Times.Once);
        }

        [Theory]
        [InlineAutoMockData(true)]
        [InlineAutoMockData(false)]
        private void ShouldCompareNotNullValueToDefaultValueWhen(
            bool expectedResult,
            Mock<object> value,
            object defaultValue,
            string name,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors)
        {
            value.Setup(v => v.Equals(It.IsAny<object>())).Returns(!expectedResult);
            value.Setup(v => v.Equals(defaultValue)).Returns(expectedResult);

            var actionArgument = new ActionArgument(name, value.Object, defaultValue, bindingSource, fieldDescriptors);

            var valueIsDefaultValue = actionArgument.ValueIsDefaultValue();

            Assert.Equal(expectedResult, valueIsDefaultValue);
        }

        [Theory]
        [InlineAutoMockData(null)]
        [InlineAutoMockData("notNullDefault")]
        private void ShouldCheckIfDefaultValueIsNullIfNullValue(
            object defaultValue,
            string name,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors)
        {
            var actionArgument = new ActionArgument(name, null, defaultValue, bindingSource, fieldDescriptors);

            actionArgument.FieldDescriptors.SetValue(null, 1);

            var expectedResult = actionArgument.DefaultValue == null;

            var valueIsDefaultValue = actionArgument.ValueIsDefaultValue();

            Assert.Equal(expectedResult, valueIsDefaultValue);
        }
    }
}