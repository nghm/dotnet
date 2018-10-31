using Hypermedia.AspNetCore.Siren.Util;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test
{
    public class RouteTemplateInterpolatorExtensionsTests
    {
        public static object[][] InterpolationCases => new object[][]
        {
            new object[] { "a", new { a = "guid", b = "nuid" }, "a" },
            new object[] { "a/{a}/a", new { a = "guid" }, "a/guid/a" },
            new object[] { "a/{a}{a}{a}/a", new { a = "guid" }, "a/guidguidguid/a" },
            new object[] { "a/{{a}/a", new { a = "guid", b = "nuid" }, "a/{guid/a" },
            new object[] { "a/{{A}/a", new { a = "guid", b = "nuid" }, "a/{{A}/a" },
            new object[] { "a/{{a:test}/a", new { a = "guid", b = "nuid" }, "a/{guid/a" },
            new object[] { "a/{{a:test}/a", new { }, "a/{{a:test}/a" }
        };

        [Theory]
        [MemberAutoMockData(nameof(InterpolationCases))]
        void ShouldInterpolateParameters(string template, object parameters, string expected) {
            Assert.Equal(template.InterpolateRouteParameters(parameters), expected);
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
