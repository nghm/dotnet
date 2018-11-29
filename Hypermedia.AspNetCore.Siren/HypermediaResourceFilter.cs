namespace Hypermedia.AspNetCore.Siren
{
    using Entities.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;

    internal class HypermediaResourceFilter : IResultFilter
    {
        private readonly ApiAwareEntityBuilder _builder;

        public HypermediaResourceFilter(ApiAwareEntityBuilder builder)
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

            resource.Configure(this._builder);

            foreach (var partialResource in partialResources)
            {
                partialResource.Configure(this._builder);
            }

            var actualResponse = this._builder.BuildAsync();

            objectResult.DeclaredType = actualResponse.GetType();
            objectResult.Value = actualResponse;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}