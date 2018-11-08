namespace Hypermedia.AspNetCore.Siren
{
    using System;
    using Microsoft.AspNetCore.Mvc.Filters;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class PartialResourceAttribute : Attribute, IActionFilter, IPartialResource
    {
        public IHypermediaResource PartialResource { get; }

        public PartialResourceAttribute(IHypermediaResource partialResource)
        {
            this.PartialResource = partialResource;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
