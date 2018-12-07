namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypermedia(this IServiceCollection services)
        {
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<IHrefFactories, HrefFactories>();

            return services;
        }
    }
}
