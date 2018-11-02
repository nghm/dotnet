using System;
using System.Collections.Generic;
using System.Text;

namespace Hypermedia.AspNetCore.Siren
{
    using System.Linq;
    using Entities;
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public static class ModelStateExtensionMethods
    {
        public static IEntity ToResource(this ModelStateDictionary modelState)
        {
            var errors = modelState
                .ToDictionary(
                    modelError => modelError.Key,
                    modelError => modelError.Value.Errors.Select(error => error.ErrorMessage) as object
                );

            return new Entity(new []{ "error" }, properties: errors);
        }
    }
}
