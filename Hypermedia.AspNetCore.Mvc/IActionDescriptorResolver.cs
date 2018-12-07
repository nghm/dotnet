namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Mvc.Abstractions;
    using System.Collections.Generic;
    using System.Reflection;

    internal interface IActionDescriptorResolver : IReadOnlyDictionary<MethodInfo, ActionDescriptor>
    {
    }
}