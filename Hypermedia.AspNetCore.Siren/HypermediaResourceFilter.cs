namespace Hypermedia.AspNetCore.Siren
{
    using System.Linq;
    using Entities.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    internal class HypermediaResourceFilter : IResultFilter
    {
        private readonly EntityBuilder _builder;

        public HypermediaResourceFilter(EntityBuilder builder)
        {
            this._builder = builder;
        }

        public void OnResultExecuting(ResultExecutingContext context)
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

            this._builder.WithClaimsPrincipal(context.HttpContext.User);

            resource.Configure(this._builder);

            foreach (var partialResource in partialResources)
            {
                partialResource.Configure(this._builder);
            }

            var actualResponse = this._builder.Build();

            objectResult.DeclaredType = actualResponse.GetType();
            objectResult.Value = actualResponse;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}