namespace Hypermedia.AspNetCore.Siren
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    internal class HypermediaResourceFilter : IResultFilter
    {
        private readonly IEntityBuilderFactory _entityBuilderFactory;

        public HypermediaResourceFilter(IEntityBuilderFactory entityBuilderFactory)
        {
            this._entityBuilderFactory = entityBuilderFactory;
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

            var builder = this._entityBuilderFactory.MakeEntity(context.HttpContext.User);

            resource.Configure(builder);

            foreach (var partialResource in partialResources)
            {
                partialResource.Configure(builder);
            }

            var actualResponse = builder.Build();

            objectResult.DeclaredType = actualResponse.GetType();
            objectResult.Value = actualResponse;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}