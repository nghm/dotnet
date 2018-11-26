using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    public class DataTypeAttributeExtractorTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullException(DataTypeAttributeExtractor sut)
        {
            Assert.Throws<ArgumentNullException>(() => sut.GetDataTypeAttribute(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnNullWhenEmpty(
            DataTypeAttributeExtractor sut)
        {
            var dataTypeAttribute = sut.GetDataTypeAttribute(new object[] { });

            Assert.Null(dataTypeAttribute);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnNullWhenNotFound(
            object[] customAttributes,
            DataTypeAttributeExtractor sut)
        {
            var dataTypeAttribute = sut.GetDataTypeAttribute(customAttributes);

            Assert.Null(dataTypeAttribute);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnMetaData(
            IEnumerable<object> customAttributes,
            DataTypeAttributeExtractor sut,
            DataTypeAttribute expectedAttribute)
        {
            customAttributes = customAttributes.Append(expectedAttribute);

            var dataTypeAttribute = sut.GetDataTypeAttribute(customAttributes.ToArray());

            Assert.Same(expectedAttribute, dataTypeAttribute);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnThrowInvalidOperationException(
            IEnumerable<DataTypeAttribute> customAttributes,
            DataTypeAttributeExtractor sut)
        {
            Assert.Throws<InvalidOperationException>(() => sut.GetDataTypeAttribute(customAttributes.ToArray<object>()));
        }
    }
}