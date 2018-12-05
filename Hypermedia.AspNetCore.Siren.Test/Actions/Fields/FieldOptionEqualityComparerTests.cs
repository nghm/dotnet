using System;
using System.Collections.Generic;
using System.Text;
using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class FieldOptionEqualityComparerTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldReturnFalseWhenOnlyFirstIsNull(
            FieldOption option,
            FieldOptionEqualityComparer sut)
        {
            var areEqual = sut.Equals(null, option);

            Assert.False(areEqual);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnFalseWhenOnlySecondIsNull(
            FieldOption option,
            FieldOptionEqualityComparer sut)
        {
            var areEqual = sut.Equals(option, null);

            Assert.False(areEqual);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnTrueWhenBothAreNull(
            FieldOptionEqualityComparer sut)
        {
            var areEqual = sut.Equals(null, null);

            Assert.True(areEqual);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnFalseWhenNamesAreDifferent(
            string name1,
            string name2,
            FieldOptionEqualityComparer sut)
        {
            var option1 = new FieldOption() { Name = name1 };
            var option2 = new FieldOption() { Name = name2 };

            var areEqual = sut.Equals(option1, option2);

            Assert.False(areEqual);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnFalseWhenValuesAreDifferent(
            object value1,
            object value2,
            FieldOptionEqualityComparer sut)
        {
            var option1 = new FieldOption() { Value = value1 };
            var option2 = new FieldOption() { Value = value2 };

            var areEqual = sut.Equals(option1, option2);

            Assert.False(areEqual);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnTrueWhenEquals(
            FieldOption option,
            FieldOptionEqualityComparer sut)
        {
            var areEqual = sut.Equals(option, option);

            Assert.True(areEqual);
        }
    }
}
