using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Moq;
using System.Linq;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using Xunit;

    public class EnumOptionsExtractorTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new EnumOptionsExtractor(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(IEnumUtilities enumUtilities)
        {
            try
            {
                var _ = new EnumOptionsExtractor(enumUtilities);
            }
            catch
            {
                Assert.True(false, "Exception was thrown when none was expected!");
            }
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenPropertyTypeIsNull(
            EnumOptionsExtractor sut
        )
        {
            Assert.Throws<ArgumentNullException>(() => sut.TryGetEnumOptions(null, out _));
        }

        [Theory]
        [AutoMockData]
        private void ShouldVerifyIfTypeIsEnumWithEnumUtils(
            Type propType,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
        )
        {
            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>()));

            var _ = sut.TryGetEnumOptions(propType, out var _);

            enumUtils.Verify(eu => eu.IsEnum(propType), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnFalseIfNotEnum(
            Type propType,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
        )
        {
            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>())).Returns(false);

            var isEnum = sut.TryGetEnumOptions(propType, out _);

            Assert.False(isEnum);
        }

        [Theory]
        [AutoMockData]
        private void ShouldOutputNullIfNotEnum(
            Type propType,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
            )
        {
            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>())).Returns(false);

            var _ = sut.TryGetEnumOptions(propType, out var options);

            Assert.Null(options);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetNamesFromEnumUtils(
            Type propType,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
        )
        {
            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>())).Returns(true);
            enumUtils.Setup(eu => eu.GetNames(It.IsAny<Type>())).Returns(new string[] { });
            enumUtils.Setup(eu => eu.GetValues(It.IsAny<Type>())).Returns(new int[] { });

            var _ = sut.TryGetEnumOptions(propType, out var _);

            enumUtils.Verify(eu => eu.GetNames(propType), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetValuesFromEnumUtils(
            Type propType,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
        )
        {
            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>())).Returns(true);
            enumUtils.Setup(eu => eu.GetNames(It.IsAny<Type>())).Returns(new string[] { });
            enumUtils.Setup(eu => eu.GetValues(It.IsAny<Type>())).Returns(new int[] { });

            var _ = sut.TryGetEnumOptions(propType, out var _);

            enumUtils.Verify(eu => eu.GetValues(propType), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldGetFieldsFromEnumUtils(
            Type propType,
            int[] values,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
        )
        {
            var expectedResult = values.Select(value => new FieldOption { Name = value.ToString(), Value = value })
                .ToArray();

            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>())).Returns(true);
            enumUtils.Setup(eu => eu.GetNames(It.IsAny<Type>()))
                .Returns(values.Select(v => v.ToString()).ToArray());
            enumUtils.Setup(eu => eu.GetValues(It.IsAny<Type>())).Returns(values);

            var _ = sut.TryGetEnumOptions(propType, out var options);

            Assert.Equal(expectedResult, options);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnTrueIfEnumType(
            Type propType,
            [Frozen]Mock<IEnumUtilities> enumUtils,
            EnumOptionsExtractor sut
        )
        {
            enumUtils.Setup(eu => eu.IsEnum(It.IsAny<Type>())).Returns(true);
            enumUtils.Setup(eu => eu.GetNames(It.IsAny<Type>())).Returns(new string[] { });
            enumUtils.Setup(eu => eu.GetValues(It.IsAny<Type>())).Returns(new int[] { });

            var isEnum = sut.TryGetEnumOptions(propType, out _);

            Assert.True(isEnum);
        }
    }
}