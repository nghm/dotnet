namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Reflection;


    [Obsolete]
    internal interface IActionDescriptorResolver
    {
        ControllerActionDescriptor Resolve(MethodInfo action);
    }
}