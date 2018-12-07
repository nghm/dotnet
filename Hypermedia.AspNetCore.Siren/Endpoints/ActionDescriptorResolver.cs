namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ActionDescriptorResolver : IActionDescriptorResolver
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorProvider;
        private Dictionary<MethodInfo, ControllerActionDescriptor> _cachedDescriptorDictionary;

        private IDictionary<MethodInfo, ControllerActionDescriptor> DescriptorDictionary
            => this._cachedDescriptorDictionary ?? (this._cachedDescriptorDictionary = MakeDescriptorDictionary());

        public ActionDescriptorResolver(IActionDescriptorCollectionProvider actionDescriptorProvider)
        {
            this._actionDescriptorProvider = actionDescriptorProvider;
        }

        private Dictionary<MethodInfo, ControllerActionDescriptor> MakeDescriptorDictionary()
        {
            var actionDescriptorsItems = this._actionDescriptorProvider
                .ActionDescriptors
                .Items
                .OfType<ControllerActionDescriptor>()
                .ToArray();

            return actionDescriptorsItems
                .ToDictionary(
                    i => i.MethodInfo,
                    i => i);
        }

        public ControllerActionDescriptor Resolve(MethodInfo actionMethodInfo)
        {
            return !this.DescriptorDictionary.ContainsKey(actionMethodInfo) ? null : this.DescriptorDictionary[actionMethodInfo];
        }
    }
}