namespace Hypermedia.AspNetCore.AsyncStepBuilder
{
    using Microsoft.Extensions.DependencyInjection;
    using System;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypermediaAsyncBuilder(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException();
            }

            services.AddSingleton(typeof(IIsolatedBuildStepExecutor<,>), typeof(IsolatedBuildStepExecutor<,>));
            services.AddTransient(typeof(IAsyncStepBuilder<,>), typeof(AsyncStepBuilder<,>));

            return services;
        }
    }
}
