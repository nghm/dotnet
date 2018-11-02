using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Siren.Entities;
using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using Xunit;

namespace Hypermedia.AspNetCore.Siren.Test
{
    public class HypermediaTests
    {
        [Theory, AutoMockData]
        void CreatesConcreteEntityBuilder(
            Hypermedia hypermedia
        )
        {
            var builder = hypermedia.MakeEntity();

            Assert.NotNull(builder);
            Assert.IsType<EntityBuilder>(builder);
        }
    }
}
