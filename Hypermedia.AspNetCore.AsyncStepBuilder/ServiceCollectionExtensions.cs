namespace Hypermedia.AspNetCore.Builder
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypermediaAsyncBuilder(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IIsolatedBuildStepExecutor<,>), typeof(IsolatedBuildStepExecutor<,>));
            services.AddTransient(typeof(IAsyncStepBuilder<,>), typeof(AsyncStepBuilder<,>));

            return services;
        }
    }
}
