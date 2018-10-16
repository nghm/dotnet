using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hypermedia.AspNetCore.Siren
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddHypermediaSiren(this IMvcBuilder mvcBuilder)
        {
            var services = mvcBuilder.Services;

            services.AddSingleton<CachedProxyCollector>();
            services.AddSingleton<IHypermedia, Hypermedia>();

            return mvcBuilder;
        }
    }
}
