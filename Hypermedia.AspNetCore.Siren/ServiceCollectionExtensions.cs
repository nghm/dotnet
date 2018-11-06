namespace Hypermedia.AspNetCore.Siren
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using ProxyCollectors;

    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddSingleton<IControllerTypeChecker, ControllerTypeChecker>();
            services.AddSingleton<IActionDescriptorResolver, ActionDescriptorResolver>();
            services.AddSingleton<IProxyCollector, ExpressionProxyCollector>();
            services.AddSingleton<IEndpointDescriptorProvider, EndpointDescriptorProvider>();
            services.AddSingleton<IEntityBuilderFactory, EntityBuilderFactory>();

            mvcBuilder.AddMvcOptions(options => { options.Filters.Add(typeof(HypermediaResourceFilter)); });

            return mvcBuilder;
        }
    }
}
