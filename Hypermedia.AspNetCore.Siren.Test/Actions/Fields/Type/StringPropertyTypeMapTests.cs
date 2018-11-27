namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields.Type
{
    using Hypermedia.AspNetCore.Siren.Actions.Fields.Type;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System.ComponentModel.DataAnnotations;
    using Xunit;

    public class StringPropertyTypeMapTests
    {
        [Theory]
        [InlineAutoMockData(DataType.CreditCard)]
        [InlineAutoMockData(DataType.Currency)]
        [InlineAutoMockData(DataType.Custom)]
        [InlineAutoMockData(DataType.Date)]
        [InlineAutoMockData(DataType.DateTime)]
        [InlineAutoMockData(DataType.Duration)]
        [InlineAutoMockData(DataType.Html)]
        [InlineAutoMockData(DataType.ImageUrl)]
        [InlineAutoMockData(DataType.MultilineText)]
        [InlineAutoMockData(DataType.PostalCode)]
        [InlineAutoMockData(DataType.Text)]
        [InlineAutoMockData(DataType.Time)]
        [InlineAutoMockData(DataType.Upload)]
        private void ShouldReturnDefaultDataType(
            DataType dataType,
            StringPropertyTypeMap sot)
        {
            var mappedType = sot.MapDataType(dataType);

            Assert.Equal("text", mappedType);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnPasswordDataType(StringPropertyTypeMap sut)
        {
            var mappedType = sut.MapDataType(DataType.Password);

            Assert.Equal("password", mappedType);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnEmailDataType(StringPropertyTypeMap sut)
        {
            var mappedType = sut.MapDataType(DataType.EmailAddress);

            Assert.Equal("email", mappedType);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnUrlDataType(StringPropertyTypeMap sut)
        {
            var mappedType = sut.MapDataType(DataType.Url);

            Assert.Equal("url", mappedType);
        }

        [Theory]
        [AutoMockData]
        private void ShouldReturnPhoneNumberDataType(StringPropertyTypeMap sut)
        {
            var mappedType = sut.MapDataType(DataType.PhoneNumber);

            Assert.Equal("phone", mappedType);
        }
    }
}