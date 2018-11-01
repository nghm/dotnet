using Hypermedia.AspNetCore.Siren.Util;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test
{
    using System.Collections.Generic;

    public class QueryParametersInterpolatorExtensionsTests
    {
        public static object[][] InterpolationCases => new[]
        {
            new object[] { "", new Dictionary<string, string>(), "" },
            new object[] { "a", new Dictionary<string, string>(), "a" },
            new object[] { "a", new Dictionary<string, string> { ["a"] = "guid" }, "a?a=guid" },
            new object[] { "a", new Dictionary<string, string> { ["a"] = "guid", ["b"] = "guid2" }, "a?a=guid&b=guid2" },
           };

        [Theory]
        [MemberAutoMockData(nameof(InterpolationCases))]
        private void ShouldInterpolateParameters(string template, Dictionary<string, string> parameters, string expected)
        {
            Assert.Equal(template.InterpolateQueryParameters(parameters), expected);
        }

        [Fact]
        private void ShouldHandleNullTarget()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                (null as string).InterpolateRouteParameters(null);
            });
        }
    }
}
