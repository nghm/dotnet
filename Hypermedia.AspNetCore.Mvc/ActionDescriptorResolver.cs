namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class ActionDescriptorResolver : Dictionary<MethodInfo, ActionDescriptor>, IActionDescriptorResolver
    {
        public ActionDescriptorResolver(IActionDescriptorCollectionProvider collection)
            : base(Make(collection.ActionDescriptors.Items))
        {
        }

        private static Dictionary<MethodInfo, ActionDescriptor> Make(IEnumerable<ActionDescriptor> descriptors)
        {
            return descriptors
                .OfType<ControllerActionDescriptor>()
                .ToDictionary(d => d.MethodInfo, d => d as ActionDescriptor);
        }
    }
}