namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class ActionProxyModule
    {
        public static IServiceCollection AddActionProxyModule(this IServiceCollection services)
        {
            services.AddSingleton<IProxifiedActionFactories, ProxifiedActionFactories>();
            services.AddSingleton<IProxifiedActions, ProxifiedActions>();

            return services;
        }
    }
}
