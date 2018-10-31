namespace Hypermedia.AspNetCore.Siren
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using ProxyCollectors;

    public class ControllerTypeChecker : IControllerTypeChecker
    {
        private readonly ISet<Type> _controllerTypes;

        public ControllerTypeChecker(IActionDescriptorCollectionProvider collectionProvider)
        {
            this._controllerTypes = collectionProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .GroupBy(d => d.ControllerTypeInfo.AsType())
                .Select(d => d.Key)
                .ToHashSet();
        }

        public bool IsController(Type controllerType)
        {
            return this._controllerTypes.Contains(controllerType);
        }
    }
}