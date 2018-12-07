namespace Hypermedia.AspNetCore.AsyncStepBuilder
{
    using Core;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    internal class IsolatedBuildStepExecutor<TBuilder, TBuilt> : IIsolatedBuildStepExecutor<TBuilder, TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public IsolatedBuildStepExecutor(IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory =
                serviceScopeFactory ??
                throw new ArgumentNullException(nameof(serviceScopeFactory));
        }

        public Task ExecuteBuildStepAsync((Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>) part, TBuilder builder)
        {
            var (serviceType, configure) = part;

            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            using (var scope = this._serviceScopeFactory.CreateScope())
            {
                var partService =
                    ((IAsyncBuildStep<TBuilder, TBuilt>)
                      scope.ServiceProvider.GetRequiredService(serviceType));

                configure(partService);

                return partService.BuildAsync(builder);
            }
        }
    }
}