namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    internal class MvcActionDescriptors : Dictionary<MethodInfo, ControllerActionDescriptor>, IMvcActionDescriptors
    {
        public MvcActionDescriptors(IActionDescriptorCollectionProvider collection)
            : base(FromEnumerable(collection.ActionDescriptors.Items))
        {
        }

        internal static Dictionary<MethodInfo, ControllerActionDescriptor> FromEnumerable(IEnumerable<ActionDescriptor> descriptors)
        {
            return descriptors
                .OfType<ControllerActionDescriptor>()
                .ToDictionary(d => d.MethodInfo, d => d);
        }
    }
}