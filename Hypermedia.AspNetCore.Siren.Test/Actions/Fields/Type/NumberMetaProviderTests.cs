namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    using AutoFixture.Xunit2;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Siren.Actions.Fields;
    using Siren.Actions.Fields.Type;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class NumberMetaProviderTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new NumberMetaProvider(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(ITypeCodeExtractor typeCodeExtractor)
        {
            try
            {
                var _ = new NumberMetaProvider(typeCodeExtractor);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenGettingMetadata(
            NumberMetaProvider sut)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = sut.GetMetadata(null)
                    .ToArray();
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetTypeCodeFromTypeCodeExtractor(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            FieldDescriptor fieldDescriptor,
            NumberMetaProvider sut)
        {
            var _ = sut.GetMetadata(fieldDescriptor)
                .ToArray();

            typeMock.Verify(t => t.GetTypeCode(fieldDescriptor.PropertyType), Times.Once);
        }

        [Theory]
        [InlineAutoMockData(TypeCode.Byte)]
        [InlineAutoMockData(TypeCode.SByte)]
        [InlineAutoMockData(TypeCode.UInt16)]
        [InlineAutoMockData(TypeCode.UInt32)]
        [InlineAutoMockData(TypeCode.UInt64)]
        [InlineAutoMockData(TypeCode.Int16)]
        [InlineAutoMockData(TypeCode.Int32)]
        [InlineAutoMockData(TypeCode.Int64)]
        [InlineAutoMockData(TypeCode.Decimal)]
        [InlineAutoMockData(TypeCode.Double)]
        [InlineAutoMockData(TypeCode.Single)]
        private void ShouldReturnMetadata(
            TypeCode typeCode,
            FieldDescriptor fieldDescriptor,
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            NumberMetaProvider sut)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<Type>()))
                .Returns(typeCode);

            var meta = sut.GetMetadata(fieldDescriptor)
                .ToArray();

            Assert.Equal(meta, new[] { new KeyValuePair<string, object>("type", "number"), });
        }

        [Theory]
        [InlineAutoMockData(TypeCode.Boolean)]
        [InlineAutoMockData(TypeCode.Char)]
        [InlineAutoMockData(TypeCode.DateTime)]
        [InlineAutoMockData(TypeCode.DBNull)]
        [InlineAutoMockData(TypeCode.Empty)]
        [InlineAutoMockData(TypeCode.Object)]
        [InlineAutoMockData(TypeCode.String)]
        private void ShouldReturnEmptyMetadata(
            TypeCode typeCode,
            FieldDescriptor fieldDescriptor,
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            NumberMetaProvider sut)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<Type>()))
                .Returns(typeCode);

            var meta = sut.GetMetadata(fieldDescriptor)
                .ToArray();

            Assert.Empty(meta);
        }
    }
}