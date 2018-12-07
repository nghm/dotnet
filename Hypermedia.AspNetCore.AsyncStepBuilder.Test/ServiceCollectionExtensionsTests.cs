namespace Hypermedia.AspNetCore.AsyncStepBuilder.Test
{
    using Core;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using System;
    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IServiceCollection).AddHypermediaAsyncBuilder());
        }

        [Theory]
        [AutoMockData]
        private void ShouldCallAddForIsolatedBuildStepExecutor(
            Mock<IServiceCollection> serviceCollection)
        {
            serviceCollection.Object.AddHypermediaAsyncBuilder();

            serviceCollection.Verify(
                s => s.Add(It.Is<ServiceDescriptor>(sd => sd.ServiceType == typeof(IIsolatedBuildStepExecutor<,>))),
                Times.Once);
        }

        [Theory]
        [AutoMockData]
        private void ShouldCallAddForAsyncStepBuilder(
            Mock<IServiceCollection> serviceCollection)
        {
            serviceCollection.Object.AddHypermediaAsyncBuilder();

            serviceCollection.Verify(
                s => s.Add(It.Is<ServiceDescriptor>(sd => sd.ServiceType == typeof(IAsyncStepBuilder<,>))),
                Times.Once);
        }

        [Fact]
        private void ShouldCreateIsolatedBuildStepExecutor()
        {
            var serviceCollection = new ServiceCollection();

            var collection = serviceCollection.AddHypermediaAsyncBuilder();

            Assert.NotNull(collection.BuildServiceProvider().GetService<IIsolatedBuildStepExecutor<IBuilder<object>, object>>());
        }
    }
}
