namespace Hypermedia.AspNetCore.Siren
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateExtensionMethods
    {
        public static IHypermediaResource AsResource(this ModelStateDictionary modelState)
        {
            return new InvalidModelStateResource(modelState);
        }
    }
}
