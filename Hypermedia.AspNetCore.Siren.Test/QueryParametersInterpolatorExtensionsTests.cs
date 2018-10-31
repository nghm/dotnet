using Hypermedia.AspNetCore.Siren.Util;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test
{
    public class QueryParametersInterpolatorExtensionsTests
    {
        public static object[][] InterpolationCases => new object[][]
           {
            new object[] { "", new { }, "" },
            new object[] { "a", new { }, "a" },
            new object[] { "a", new { a = "guid" }, "a?a=guid" },
            new object[] { "a", new { a = "guid", b = "guid2" }, "a?a=guid&b=guid2" },
           };

        [Theory]
        [MemberAutoMockData(nameof(InterpolationCases))]
        void ShouldInterpolateParameters(string template, object parameters, string expected)
        {
            Assert.Equal(template.InterpolateQueryParameters(parameters), expected);
        }

        [Fact]
        void ShouldHandleNullTarget()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                (null as string).InterpolateRouteParameters(null);
            });
        }
    }
}
