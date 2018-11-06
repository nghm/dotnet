namespace Hypermedia.AspNetCore.Siren
{
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

            var builder = this._entityBuilderFactory.MakeEntity(context.HttpContext.User);

            resource.Configure(builder);

            var actualResponse = builder.Build();

            objectResult.DeclaredType = actualResponse.GetType();
            objectResult.Value = actualResponse;
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }
    }
}