namespace Hypermedia.AspNetCore.Siren
{
    using Entities.Builder;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Linq;

    internal class InvalidModelStateResource : IHypermediaResource
    {
        private readonly ModelStateDictionary _modelState;

        public InvalidModelStateResource(ModelStateDictionary modelState)
        {
            this._modelState = modelState;
        }

        public void Configure(IApiAwareEntityBuilder builder)
        {
            var errors = this._modelState
                .ToDictionary(
                    modelError => modelError.Key,
                    modelError => modelError.Value.Errors.Select(error => error.ErrorMessage) as object
                );

            builder.WithProperties(new { errors });
        }
    }
}
