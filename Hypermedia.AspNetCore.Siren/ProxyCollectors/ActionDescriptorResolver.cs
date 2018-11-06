namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ActionDescriptorResolver : IActionDescriptorResolver
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorProvider;
        private Dictionary<Type, Dictionary<MethodInfo, ControllerActionDescriptor>> _cachedDescriptorDictionary;

        private IDictionary<Type, Dictionary<MethodInfo, ControllerActionDescriptor>> DescriptorDictionary 
            => this._cachedDescriptorDictionary ?? (this._cachedDescriptorDictionary = MakeDescriptorDictionary());

        public ActionDescriptorResolver(IActionDescriptorCollectionProvider actionDescriptorProvider)
        {
            this._actionDescriptorProvider = actionDescriptorProvider;
        }

        private Dictionary<Type, Dictionary<MethodInfo, ControllerActionDescriptor>> MakeDescriptorDictionary()
        {
            var actionDescriptorsItems = this._actionDescriptorProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .ToArray();

            return actionDescriptorsItems
                .GroupBy(d => d.ControllerTypeInfo.AsType())
                .ToDictionary(
                    i => i.Key,
                    i => i.ToDictionary(k => k.MethodInfo, k => k)
                );
        }

        public ControllerActionDescriptor Resolve(Type controllerType, MethodInfo actionMethodInfo)
        {
            if (!this.DescriptorDictionary.ContainsKey(controllerType))
            {
                return null;
            }

            var controllerActionDescriptors = this.DescriptorDictionary[controllerType];

            if (!controllerActionDescriptors.ContainsKey(actionMethodInfo))
            {
                return null;
            }

            return controllerActionDescriptors[actionMethodInfo];
        }
    }
}