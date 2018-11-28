namespace Hypermedia.AspNetCore.Siren.Test.Parallel
{
    using AutoFixture.Xunit2;
    using Entities;
    using Entities.Builder;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Siren.Parallel;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Utils;
    using Xunit;

    public class ParallelEntityBuilderTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(IEntityPartStorage storage, IEntityBuilder builder)
        {
            AssertUtils.NoExceptions(() =>
            {
                var _ = new ParallelEntityBuilder(storage, builder);
            });
        }

        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForPartStorage(IEntityBuilder builder)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ParallelEntityBuilder(null, builder);
            });
        }


        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForEntityBuilder(IEntityPartStorage storage)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new ParallelEntityBuilder(storage, null);
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrownArgumentNullException(
            ParallelEntityBuilder sut
        )
        {
            Assert.Throws<ArgumentNullException>(() => sut.AddBuildPart(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldAddBuildPart(
            [Frozen] Mock<IEntityPartStorage> storage,
            IEntityBuildPart part,
            ParallelEntityBuilder sut
        )
        {
            sut.AddBuildPart(part);

            storage.Verify(s => s.Add(part), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldCallBuildOnPartsInParallel(
            Mock<IEntityBuildPart>[] parts,
            [Frozen] Mock<IEntityBuilder> builder,
            [Frozen] Mock<IEntityPartStorage> storage,
            ParallelEntityBuilder sut
        )
        {
            storage
                .Setup(s => s.GetEnumerator())
                .Returns(parts.Select(m => m.Object).GetEnumerator());

            var _ = sut.BuildAsync();

            foreach (var part in parts)
            {
                part.Verify(p => p.BuildAsync(builder.Object), Times.Once);
            }
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldBuildEntity(
            IEntity expectedEntity,
            [Frozen] Mock<IEntityBuilder> builder,
            ParallelEntityBuilder sut
        )
        {
            builder
                .Setup(b => b.Build())
                .Returns(expectedEntity);

            var entity = await sut.BuildAsync();

            Assert.Same(entity, expectedEntity);
        }
    }
}
