namespace Hypermedia.AspNetCore.Siren.Test
{
    using System;
    using System.Collections.Generic;
    using Entities.Builder;
    using Newtonsoft.Json;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Xunit;

    public class EntityBuilderTests
    {
        [Theory]
        [AutoMockData]
        private void WithClassesAddsClassesInConsistentOrder(
            List<string> classes,
            EntityBuilder builder
        )
        {
            builder.WithClasses(classes.ToArray());

            var result = builder.Build();

            var resultString = JsonConvert.SerializeObject(result);
            var classesArray = JsonConvert.SerializeObject(classes);

            Assert.Contains(classesArray, resultString);
        }

        [Theory]
        [AutoMockData]
        private void WithPropertiesAddsPropertiesInConsistentOrder(
            EntityBuilder builder
        )
        {
            var date = DateTime.Now;

            var properties = new
            {
                a = "123",
                A = "1234",
                _B = 1234,
                date
            };

            var propertiesDeDuped = new
            {
                a = "1234",
                _B = 1234,
                date
            };

            builder.WithProperties(properties);

            var result = builder.Build();

            var resultString = JsonConvert.SerializeObject(result);
            var propertiesObject = JsonConvert.SerializeObject(propertiesDeDuped);

            Assert.Contains(propertiesObject, resultString);
        }
    }
}
