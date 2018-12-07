using Hypermedia.AspNetCore.Store;

namespace Hypermedia.AspNetCore.Core.Test
{
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoFakeItEasy.Attributes;
    using System;
    using Xunit;

    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => (null as IServiceCollection).AddHypermediaStorage());
        }

        [Theory]
        [AutoMockData]
        private void ShouldCallAddTransient(
            Mock<IServiceCollection> serviceCollection)
        {
            serviceCollection.Object.AddHypermediaStorage();

            serviceCollection.Verify(
                s => s.Add(It.Is<ServiceDescriptor>(sd => sd.ServiceType == typeof(IStorage<>))),
                Times.Once);
        }

        [Fact]
        private void ShouldCreateStorageService()
        {
            var serviceCollection = new ServiceCollection();

            var collection = serviceCollection.AddHypermediaStorage();

            Assert.NotNull(collection.BuildServiceProvider().GetService<IStorage<object>>());
        }
    }
}
