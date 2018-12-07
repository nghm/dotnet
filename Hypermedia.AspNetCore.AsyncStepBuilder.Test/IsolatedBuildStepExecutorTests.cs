using AutoFixture.Xunit2;
using Hypermedia.AspNetCore.Core;
using Hypermedia.AspNetCore.Tests.Common;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Hypermedia.AspNetCore.AsyncStepBuilder.Test
{
    public class IsolatedBuildStepExecutorTests
    {
        [Fact]
        private void ShouldThrowArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new IsolatedBuildStepExecutor<IBuilder<object>, object>(null));
        }

        [Theory]
        [AutoMockData]
        private void ShouldCreateInstance(
            IServiceScopeFactory serviceScopeFactory)
        {
            AssertUtils.NoExceptions(() =>
                new IsolatedBuildStepExecutor<IBuilder<object>, object>(serviceScopeFactory));
        }

        [Theory]
        [AutoMockData]
        private async Task ShouldCreateScopeUsingFactory(
            Type serviceType,
            IBuilder<object> builder,
            Action<IAsyncBuildStep<IBuilder<object>, object>> configure,
            [Frozen]Mock<IServiceScopeFactory> serviceScopeFactory,
            IsolatedBuildStepExecutor<IBuilder<object>, object> sut)
        {
            await AssertUtils.IgnoreExceptionAsync<InvalidCastException>(() =>
               sut.ExecuteBuildStepAsync((serviceType, configure), builder)
            );

            serviceScopeFactory.Verify(f => f.CreateScope(), Times.Once);
        }
    }
}
