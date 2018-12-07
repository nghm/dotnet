namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using System.Collections.Generic;
    using System.Reflection;

    internal interface IMvcActionDescriptors : IReadOnlyDictionary<MethodInfo, ControllerActionDescriptor>
    {
    }
}