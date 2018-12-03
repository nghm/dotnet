namespace Hypermedia.AspNetCore.Siren
{
    using Hypermedia.AspNetCore.Siren.Resources;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    internal class HypermediaResourceFilter : IAsyncResultFilter
    {
        private readonly IServiceScopeFactory _factory;

        public HypermediaResourceFilter(IServiceScopeFactory factory)
        {
            this._factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task OnResultExecutingAsync(ResultExecutingContext context)
        {
            await Task.CompletedTask;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!(context.Result is ObjectResult result) ||
                !(result.Value is IHypermediaResource resource))
            {
                return;
            }

            var partialResources = GetPartialResources(context);

            using (var scope = this._factory.CreateScope())
            {
                var builder = scope.ServiceProvider.GetService<IResourceBuilder>();

                resource.Configure(builder);

                foreach (var partialResource in partialResources)
                {
                    partialResource.Configure(builder);
                }

                var actualResponse = await builder.BuildAsync();

                result.DeclaredType = actualResponse.GetType();
                result.Value = actualResponse;

            }

            await next();
        }

        private static IEnumerable<IHypermediaResource> GetPartialResources(ResultExecutingContext context)
        {
            return context
                .ActionDescriptor
                .FilterDescriptors
                .Select(filter => filter.Filter)
                .OfType<IPartialResource>()
                .Select(filter => filter.PartialResource);
        }
    }
}