namespace Hypermedia.AspNetCore.Siren
{
    using Microsoft.Extensions.DependencyInjection;
    using ProxyCollectors;

    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddSingleton<IControllerTypeChecker, ControllerTypeChecker>();
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<IProxyCollector, CastleProxyCollector>();
            services.AddSingleton<IEndpointDescriptorProvider, EndpointDescriptorProvider>();
            services.AddSingleton<IHypermedia, Hypermedia>();

            return mvcBuilder;
        }
    }
}
