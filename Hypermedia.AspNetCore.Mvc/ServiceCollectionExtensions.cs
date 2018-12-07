namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypermediaMvc(this IServiceCollection services)
        {
            services.AddSingleton<IHrefFactories, HrefFactories>();

            return services;
        }
    }
}
