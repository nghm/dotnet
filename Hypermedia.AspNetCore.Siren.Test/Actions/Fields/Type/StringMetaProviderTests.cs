namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    using AutoFixture.Xunit2;
    using Hypermedia.AspNetCore.Siren.Actions.Fields;
    using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using Xunit;

    public class StringMetaProviderTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionForStringPropertyTypeExtractor(
            ITypeCodeExtractor typeCodeExtractor,
            IDataTypeAttributeExtractor dataTypeAttributeExtractor
            )
        {
            Assert.Throws<ArgumentNullException>(() => new StringMetaProvider(null, typeCodeExtractor, dataTypeAttributeExtractor));
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionForTypeCodeExtractor(
            IStringPropertyTypeMap stringPropertyTypeMap,
            IDataTypeAttributeExtractor dataTypeAttributeExtractor
            )
        {
            Assert.Throws<ArgumentNullException>(() => new StringMetaProvider(stringPropertyTypeMap, null, dataTypeAttributeExtractor));
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionForDataTypeAtrtibuteExtractor(
            ITypeCodeExtractor typeCodeExtractor,
            IStringPropertyTypeMap stringPropertyTypeMap
        )
        {
            Assert.Throws<ArgumentNullException>(() => new StringMetaProvider(stringPropertyTypeMap, typeCodeExtractor, null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IStringPropertyTypeMap stringPropertyTypeMap,
            ITypeCodeExtractor typeCodeExtractor,
            IDataTypeAttributeExtractor dataTypeAttributeExtractor
            )
        {
            try
            {
                var _ = new StringMetaProvider(stringPropertyTypeMap, typeCodeExtractor, dataTypeAttributeExtractor);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenGettingMetadata(
            StringMetaProvider sut
            )
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetMetadata(null).ToArray());
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetTypeCodeFromTypeCodeExtractor(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider sut)
        {
            var _ = sut.GetMetadata(fieldGenerationContext)
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
        [InlineAutoMockData(TypeCode.Boolean)]
        [InlineAutoMockData(TypeCode.Char)]
        [InlineAutoMockData(TypeCode.DateTime)]
        [InlineAutoMockData(TypeCode.DBNull)]
        [InlineAutoMockData(TypeCode.Empty)]
        [InlineAutoMockData(TypeCode.Object)]
        private void ShouldReturnEmptyMetadata(
            TypeCode typeCode,
            FieldGenerationContext fieldGenerationContext,
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            StringMetaProvider sut)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(typeCode);

            var meta = sut.GetMetadata(fieldGenerationContext)
                .ToArray();

            Assert.Empty(meta);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetDataTypeAttributeFromDataTypeAttributeExtractor(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            [Frozen] Mock<IDataTypeAttributeExtractor> dataTypeMock,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider sut)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);

            var _ = sut.GetMetadata(fieldGenerationContext)
                .ToArray();

            dataTypeMock.Verify(t => t.GetDataTypeAttribute(fieldGenerationContext.FieldDescriptor.CustomAttributes), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldFallbackToTypeText(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            [Frozen] Mock<IDataTypeAttributeExtractor> dataTypeMock,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider sut)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);
            dataTypeMock.Setup(t => t.GetDataTypeAttribute(fieldGenerationContext.FieldDescriptor.CustomAttributes))
                .Returns(null as DataTypeAttribute);

            var meta = sut.GetMetadata(fieldGenerationContext)
                .ToArray();

            Assert.Equal(new[]
            {
                new KeyValuePair<string, object>("type", "text")
            }, meta);
        }

        [Theory]
        [AutoMockData]
        private void ShouldPassDataTypeToStringPropertyTypeMap(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            [Frozen] Mock<IDataTypeAttributeExtractor> dataTypeMock,
            [Frozen] Mock<IStringPropertyTypeMap> stringPropertyTypeMap,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider sut,
            DataTypeAttribute expectedDataTypeAttribute)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);
            dataTypeMock.Setup(t => t.GetDataTypeAttribute(fieldGenerationContext.FieldDescriptor.CustomAttributes))
                .Returns(expectedDataTypeAttribute);

            var _ = sut.GetMetadata(fieldGenerationContext)
                .ToArray();

            stringPropertyTypeMap.Verify(m => m.MapDataType(expectedDataTypeAttribute.DataType), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetadataFromTypeMap(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            [Frozen] Mock<IDataTypeAttributeExtractor> dataTypeMock,
            [Frozen] Mock<IStringPropertyTypeMap> stringPropertyTypeMap,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider sut,
            DataTypeAttribute expectedDataTypeAttribute,
            string type)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);
            dataTypeMock.Setup(t => t.GetDataTypeAttribute(fieldGenerationContext.FieldDescriptor.CustomAttributes))
                .Returns(expectedDataTypeAttribute);
            stringPropertyTypeMap.Setup(m => m.MapDataType(expectedDataTypeAttribute.DataType))
                .Returns(type);

            var meta = sut.GetMetadata(fieldGenerationContext)
                .ToArray();

            Assert.Equal(new[]
            {
                new KeyValuePair<string, object>("type", type)
            }, meta);
        }
    }
}