namespace Hypermedia.AspNetCore.Siren
{
    using Hypermedia.AspNetCore.Siren.Resources;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.DependencyInjection;
    using System.Linq;
    using System.Threading.Tasks;

    internal class HypermediaResourceFilter : IAsyncResultFilter
    {
        private readonly IServiceScopeFactory _factory;

        public HypermediaResourceFilter(IServiceScopeFactory factory)
        {
            this._factory = factory;
        }

        public Task OnResultExecutingAsync(ResultExecutingContext context)
        {
            return Task.CompletedTask;
        }

        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!(context.Result is ObjectResult objectResult))
            {
                return;
            }

            if (!(objectResult.Value is IHypermediaResource resource))
            {
                return;
            }

            var partialResources = context
                .ActionDescriptor
                .FilterDescriptors
                .Select(filter => filter.Filter)
                .OfType<IPartialResource>()
                .Select(filter => filter.PartialResource);

            using (var scope = this._factory.CreateScope())
            {
                var provider = scope.ServiceProvider as ServiceCollection;

                var builder = scope.ServiceProvider.GetService<IResourceBuilder>();

                resource.Configure(builder);

                foreach (var partialResource in partialResources)
                {
                    partialResource.Configure(builder);
                }

                var actualResponse = await builder.BuildAsync();

                objectResult.DeclaredType = actualResponse.GetType();
                objectResult.Value = actualResponse;

            }

            await next();
        }
    }
}