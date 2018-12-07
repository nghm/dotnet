namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class Module
    {
        public static IServiceCollection AddApiExploration(this IServiceCollection services)
        {
            services.AddSingleton<IApiActionDescriptors, ApiActionDescriptors>();
            services.AddSingleton<IMvcActionDescriptors, MvcActionDescriptors>();

            return services;
        }
    }
}
