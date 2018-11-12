namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal interface IActionDescriptorResolver
    {
        ControllerActionDescriptor Resolve(Type controllerType, MethodInfo actionMethodInfo);
    }
}