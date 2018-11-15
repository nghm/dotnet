namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal interface IActionFactory
    {
        ActionDefinition Make(ControllerActionDescriptor actionDescriptor);
    }
}