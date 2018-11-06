namespace Hypermedia.AspNetCore.Siren
{
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    internal class InvalidModelStateResource : HypermediaResource
    {
        private readonly ModelStateDictionary _modelState;

        public InvalidModelStateResource(ModelStateDictionary modelState)
        {
            this._modelState = modelState;
        }

        public void Configure(IEntityBuilder builder)
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
