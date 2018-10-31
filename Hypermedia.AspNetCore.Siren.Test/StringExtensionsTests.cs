using Hypermedia.AspNetCore.Siren.Util;
using System;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("a", "a")]
        [InlineData("A", "a")]
        [InlineData("aA", "aA")]
        [InlineData("AA", "aA")]
        [InlineData("1", "1")]
        [InlineData("11", "11")]
        [InlineData("$", "$")]
        [InlineData("$$", "$$")]
        [InlineData("", "")]
        void ShouldCamelCasesStrings(string from, string expected)
        {
            Assert.Equal(from.ToCamelCase(), expected);
        }

        [Fact]
        void ShouldHandleNullTarget()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                (null as string).ToCamelCase();
            });
        }
    }
}
