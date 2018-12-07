namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;

    internal interface IActionDescriptorResolver
    {
        ControllerActionDescriptor Resolve(MethodInfo action);
    }
}