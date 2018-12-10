namespace Hypermedia.AspNetCore.Mvc.HrefProviders
{
    using Microsoft.Extensions.DependencyInjection;

    public static class HrefModule
    {
        public static IServiceCollection AddHypermediaMvc(this IServiceCollection services)
        {
            services.AddSingleton<IHrefProviders, HrefProviders>();

            return services;
        }
    }
}
