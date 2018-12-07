using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Hypermedia.AspNetCore.Tests.Common;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class FieldsFactoryTests
    {
        [Fact]
        private void ShouldThrowArgumentNullExceptionWhenFieldFactoryIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new FieldsFactory(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IFieldFactory fieldFactory)
        {
            AssertUtils.NoExceptions(() => new FieldsFactory(fieldFactory));
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenBodyArgumentIsNull(
            FieldsFactory sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.MakeFields(null).ToList());
        }

        [Theory]
        [AutoMockData]
        private void ShouldMakeFieldsUsingFieldFactory(
            [Frozen]FieldDescriptor[] fieldDescriptors,
            ActionArgument bodyArgument,
            [Frozen]Mock<IFieldFactory> fieldFactory,
            FieldsFactory sut)
        {
            var _ = sut.MakeFields(bodyArgument).ToList();

            foreach (var fieldDescriptor in fieldDescriptors)
            {
                fieldFactory.Verify(ff => ff.MakeField(fieldDescriptor), Times.Once);
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldMakeFieldsForAllDescriptors(
            Field[] expectedResult,
            ActionArgument bodyArgument,
            [Frozen]Mock<IFieldFactory> fieldFactory,
            FieldsFactory sut)
        {
            var expectedFieldQueue = new Queue<Field>(expectedResult);

            fieldFactory.Setup(f => f.MakeField(It.IsAny<FieldDescriptor>()))
                .Returns(expectedFieldQueue.Dequeue);

            var fields = sut.MakeFields(bodyArgument).ToList();

            Assert.Equal(expectedResult, fields);
        }
    }
}
