using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Hypermedia.AspNetCore.Siren
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddSingleton<IControllerTypeChecker, ControllerTypeChecker>();
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<IProxyCollector, CachedProxyCollector>();
            services.AddSingleton<IHypermedia, Hypermedia>();

            return mvcBuilder;
        }
    }
}
