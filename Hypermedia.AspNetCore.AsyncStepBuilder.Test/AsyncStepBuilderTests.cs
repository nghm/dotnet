namespace Hypermedia.AspNetCore.Siren.Test.Parallel
{
    using AutoFixture.Xunit2;
    using Builder;
    using Hypermedia.AspNetCore.Tests.Common;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Store;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    public class AsyncStepBuilderTests
    {
        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForPartStorage(
            IIsolatedBuildStepExecutor<IBuilder<object>, object> stepExecutor,
            IBuilder<object> builder
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new AsyncStepBuilder<IBuilder<object>, object>(null, builder, stepExecutor);
            });
        }


        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForEntityBuilder(
            IIsolatedBuildStepExecutor<IBuilder<object>, object> stepExecutor,
            IStorage<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)> storage
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new AsyncStepBuilder<IBuilder<object>, object>(storage, null, stepExecutor);
            });
        }

        [Theory]
        [AutoMockData]
        private void CreateShouldThrowArgumentNullExceptionForApplier(
            IStorage<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)> storage,
            IBuilder<object> builder
        )
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new AsyncStepBuilder<IBuilder<object>, object>(storage, builder, null);
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IStorage<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)> storage,
            IBuilder<object> builder,
            IIsolatedBuildStepExecutor<IBuilder<object>, object> stepExecutor)
        {
            AssertUtils.NoExceptions(() =>
            {
                var _ = new AsyncStepBuilder<IBuilder<object>, object>(storage, builder, stepExecutor);
            });
        }

        [Theory]
        [AutoMockData]
        private void ShouldThrowArgumentNullExceptionWhenAddingNullBuildStep(
            AsyncStepBuilder<IBuilder<object>, object> sut
        )
        {
            Assert.Throws<ArgumentNullException>(() => sut.AddStep<IAsyncBuildStep<IBuilder<object>, object>>(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldAddBuildPartWithForwardActionReference(
            [Frozen] Mock<IStorage<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)>> storage,
            Mock<Action<IAsyncBuildStep<IBuilder<object>, object>>> action,
            IAsyncBuildStep<IBuilder<object>, object> expectedClassesStep,
            AsyncStepBuilder<IBuilder<object>, object> sut)
        {
            // Arrange
            (Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)? stored = null;
            storage
                .Setup(s => s.Add(It.IsAny<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)>()))
                .Callback<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)>(s =>
                {
                    stored = s;
                });

            // Act
            sut.AddStep(action.Object);

            stored?.Item2(expectedClassesStep);

            // Assert
            Assert.Same(stored?.Item1, typeof(IAsyncBuildStep<IBuilder<object>, object>));
            action.Verify(a => a(expectedClassesStep), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldEnumerateParts(
            [Frozen] Mock<IStorage<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)>> storage,
            AsyncStepBuilder<IBuilder<object>, object> sut)
        {
            await sut.BuildAsync();

            storage
                .Verify(s => s.GetEnumerator(), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldApplyScopedBuildForEachPart(
            List<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)> steps,
            [Frozen] IBuilder<object> builder,
            [Frozen] Mock<IIsolatedBuildStepExecutor<IBuilder<object>, object>> applier,
            [Frozen] Mock<IStorage<(Type, Action<IAsyncBuildStep<IBuilder<object>, object>>)>> storage,
            AsyncStepBuilder<IBuilder<object>, object> sut)
        {
            storage
                .Setup(s => s.GetEnumerator())
                .Returns(steps.GetEnumerator());

            await sut.BuildAsync();

            foreach (var step in steps)
            {
                applier.Verify(a => a.ExecuteBuildStepAsync(step, builder), Times.Once);
            }
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldCallEntityBuilderBuild(
            [Frozen] Mock<IBuilder<object>> builder,
            AsyncStepBuilder<IBuilder<object>, object> sut
        )
        {
            var entity = await sut.BuildAsync();

            builder.Verify(b => b.Build(), Times.Once);
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldReturnEntityBuiltByEntityBuilder(
            object expectedEntity,
            [Frozen] Mock<IBuilder<object>> builder,
            AsyncStepBuilder<IBuilder<object>, object> sut
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
