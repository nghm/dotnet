namespace Hypermedia.AspNetCore.Siren.Test.Parallel
{
    using AutoFixture.Xunit2;
    using Entities;
    using Entities.Builder;
    using Entities.Builder.Steps;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Siren.Parallel;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Utils;
    using Xunit;

    public class ParallelBuildingEnvironmentTests
    {
        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IStorage<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)> storage,
            IEntityBuilder builder,
            IScopedBuildApplier<IEntityBuilder, IEntity> applier)
        {
            AssertUtils.NoExceptions(() =>
            {
                var _ = new AsyncBuildingEnvironment<IEntityBuilder, IEntity>(storage, builder, applier);
            });
        }

        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForPartStorage(
            IScopedBuildApplier<IEntityBuilder, IEntity> applier,
            IEntityBuilder builder
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new AsyncBuildingEnvironment<IEntityBuilder, IEntity>(null, builder, applier);
            });
        }


        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForEntityBuilder(
            IScopedBuildApplier<IEntityBuilder, IEntity> applier,
            IStorage<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)> storage
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new AsyncBuildingEnvironment<IEntityBuilder, IEntity>(storage, null, applier);
            });
        }

        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForApplier(
            IStorage<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)> storage,
            IEntityBuilder builder
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new AsyncBuildingEnvironment<IEntityBuilder, IEntity>(storage, builder, null);
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrownArgumentNullException(
            AsyncBuildingEnvironment<IEntityBuilder, IEntity> sut
        )
        {
            Assert.Throws<ArgumentNullException>(() => sut.AddAsyncBuildStep<AddClassesStep>(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldAddBuildPartWithFrowardActionReference(
            [Frozen] Mock<IStorage<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)>> storage,
            Mock<Action<AddClassesStep>> action,
            AddClassesStep expectedClassesStep,
            AsyncBuildingEnvironment<IEntityBuilder, IEntity> sut)
        {
            Action<AddClassesStep> capturedAction = null;

            storage.Setup(s => s.Add(It.IsAny<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)>()))
                .Callback<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)>(t =>
                {
                    // Store the composed action into a captured action
                    capturedAction = t.Item2;
                });

            sut.AddAsyncBuildStep(action.Object);

            storage
                .Verify(s => s.Add(
                    It.Is<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)>(
                        step => step.Item1 == typeof(AddClassesStep)
                    )
                ),
                Times.Once);

            Assert.NotNull(capturedAction);

            // Call captured action to check that the mock action is actually part of it's implementation
            capturedAction(expectedClassesStep);

            action.Verify(a => a(expectedClassesStep), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldEnumerateParts(
            [Frozen] Mock<IStorage<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)>> storage,
            AsyncBuildingEnvironment<IEntityBuilder, IEntity> sut)
        {
            await sut.BuildAsync();

            storage
                .Verify(s => s.GetEnumerator(), Times.Once);


        }

        [Theory]
        [AutoMockData]
        private async Task ShouldApplyScopedBuildForEachPart(
            List<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)> steps,
            [Frozen] IEntityBuilder builder,
            [Frozen] Mock<IScopedBuildApplier<IEntityBuilder, IEntity>> applier,
            [Frozen] Mock<IStorage<(Type, Action<IAsyncBuildStep<IEntityBuilder, IEntity>>)>> storage,
            AsyncBuildingEnvironment<IEntityBuilder, IEntity> sut)
        {
            storage
                .Setup(s => s.GetEnumerator())
                .Returns(steps.GetEnumerator());

            await sut.BuildAsync();

            foreach (var step in steps)
            {
                applier.Verify(a => a.ApplyScopedBuild(step, builder), Times.Once);
            }
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldBuildEntity(
            IEntity expectedEntity,
            [Frozen] Mock<IEntityBuilder> builder,
            AsyncBuildingEnvironment<IEntityBuilder, IEntity> sut
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
