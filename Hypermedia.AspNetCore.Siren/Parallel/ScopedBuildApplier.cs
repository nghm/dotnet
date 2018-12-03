namespace Hypermedia.AspNetCore.Siren.Parallel
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    internal interface IScopedBuildApplier<TBuilder, TBuilt> where TBuilder : class, IBuilder<TBuilt> where TBuilt : class
    {
        Task ApplyScopedBuild((Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>) part, TBuilder builder);
    }

    internal class ScopedBuildApplier<TBuilder, TBuilt> : IScopedBuildApplier<TBuilder, TBuilt> where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ScopedBuildApplier(IServiceScopeFactory serviceScopeFactory)
        {
            this._serviceScopeFactory = serviceScopeFactory;
        }

        public Task ApplyScopedBuild((Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>) part, TBuilder builder)
        {
            var (serviceType, configure) = part;

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