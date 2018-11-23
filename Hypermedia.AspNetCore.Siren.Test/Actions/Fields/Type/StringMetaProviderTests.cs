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

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
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
            StringMetaProvider stringMetaProvider
            )
        {
            Assert.Throws<ArgumentNullException>(() => stringMetaProvider.GetMetadata(null).ToArray());
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetTypeCodeFromTypeCodeExtractor(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            var _ = stringMetaProvider.GetMetadata(fieldGenerationContext)
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
            StringMetaProvider stringMetaProvider)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(typeCode);

            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext)
                .ToArray();

            Assert.Empty(meta);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetDataTypeAttributeFromDataTypeAttributeExtractor(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            [Frozen] Mock<IDataTypeAttributeExtractor> dataTypeMock,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);

            var _ = stringMetaProvider.GetMetadata(fieldGenerationContext)
                .ToArray();

            dataTypeMock.Verify(t => t.GetDataTypeAttribute(fieldGenerationContext), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldFallbackToTypeText(
            [Frozen] Mock<ITypeCodeExtractor> typeMock,
            [Frozen] Mock<IDataTypeAttributeExtractor> dataTypeMock,
            FieldGenerationContext fieldGenerationContext,
            StringMetaProvider stringMetaProvider)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);
            dataTypeMock.Setup(t => t.GetDataTypeAttribute(fieldGenerationContext))
                .Returns(null as DataTypeAttribute);

            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext)
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
            StringMetaProvider stringMetaProvider,
            DataTypeAttribute expectedDataTypeAttribute)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);
            dataTypeMock.Setup(t => t.GetDataTypeAttribute(fieldGenerationContext))
                .Returns(expectedDataTypeAttribute);

            var _ = stringMetaProvider.GetMetadata(fieldGenerationContext)
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
            StringMetaProvider stringMetaProvider,
            DataTypeAttribute expectedDataTypeAttribute,
            string type)
        {
            typeMock.Setup(t => t.GetTypeCode(It.IsAny<System.Type>()))
                .Returns(TypeCode.String);
            dataTypeMock.Setup(t => t.GetDataTypeAttribute(fieldGenerationContext))
                .Returns(expectedDataTypeAttribute);
            stringPropertyTypeMap.Setup(m => m.MapDataType(expectedDataTypeAttribute.DataType))
                .Returns(type);

            var meta = stringMetaProvider.GetMetadata(fieldGenerationContext)
                .ToArray();

            Assert.Equal(new[]
            {
                new KeyValuePair<string, object>("type", type)
            }, meta);
        }
    }
}