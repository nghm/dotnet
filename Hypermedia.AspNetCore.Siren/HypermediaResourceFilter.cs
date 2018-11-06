namespace Hypermedia.AspNetCore.Siren
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class HypermediaResourceFilter : IResultFilter
    {
        private readonly IHypermedia _hypermedia;

        public HypermediaResourceFilter(IHypermedia hypermedia)
        {
            this._hypermedia = hypermedia;
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (!(context.Result is ObjectResult objectResult))
            {
                return;
            }

            if (!(objectResult.Value is HypermediaResource resource))
            {
                return;
            }

            var builder = this._hypermedia.MakeEntity(context.HttpContext.User);

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