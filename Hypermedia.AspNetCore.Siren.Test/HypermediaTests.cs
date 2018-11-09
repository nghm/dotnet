namespace Hypermedia.AspNetCore.Siren.Test
{
    using Entities;
    using Entities.Builder;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Xunit;

    public class HypermediaTests
    {
        [Theory, AutoMockData]
        void CreatesConcreteEntityBuilder(EntityBuilderFactory entityBuilderFactory
        )
        {
            var builder = entityBuilderFactory.MakeEntity();

            Assert.NotNull(builder);
            Assert.IsType<EntityBuilder>(builder);
        }
    }
}
