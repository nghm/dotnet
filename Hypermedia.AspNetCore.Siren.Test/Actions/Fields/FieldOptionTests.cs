using Hypermedia.AspNetCore.Siren.Actions.Fields;
using Newtonsoft.Json.Serialization;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System.Reflection;
using Newtonsoft.Json;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test.Actions.Fields
{
    public class FieldOptionTests
    {
        [Theory]
        [AutoMockData]
        private void ValueShouldBeSerialized(
            FieldOption sut)
        {
            var result = JsonConvert.SerializeObject(sut);

            Assert.Contains(
                "\"value\":" + JsonConvert.SerializeObject(sut.Value), 
                result);
        }

        [Theory]
        [AutoMockData]
        private void NameShouldBeSerialized(
            FieldOption sut)
        {
            var result = JsonConvert.SerializeObject(sut);

            Assert.Contains(
                "\"name\":\"" + sut.Name+ "\"",
                result);
        }
    }
}