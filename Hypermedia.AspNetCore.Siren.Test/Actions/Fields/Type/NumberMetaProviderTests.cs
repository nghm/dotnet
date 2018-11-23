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
            NumberMetaProvider numberMetaProvider)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = numberMetaProvider.GetMetadata(null)
                    .ToArray();
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetTypeCodeFromTypeCodeExtractor(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            FieldGenerationContext fieldGenerationContext,
            NumberMetaProvider numberMetaProvider)
        {
            var _ = numberMetaProvider.GetMetadata(fieldGenerationContext)
                .ToArray();

            typeMock.Verify(t => t.GetTypeCode(fieldGenerationContext.FieldDescriptor.PropertyType), Times.Once);
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
            FieldGenerationContext fieldGenerationContext,
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            NumberMetaProvider numberMetaProvider)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<Type>()))
                .Returns(typeCode);

            var meta = numberMetaProvider.GetMetadata(fieldGenerationContext)
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
            FieldGenerationContext fieldGenerationContext,
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            NumberMetaProvider numberMetaProvider)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<Type>()))
                .Returns(typeCode);

            var meta = numberMetaProvider.GetMetadata(fieldGenerationContext)
                .ToArray();

            Assert.Empty(meta);
        }
    }
}