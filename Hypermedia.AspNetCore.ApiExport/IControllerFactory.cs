namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc.Controllers;

    internal interface IControllerFactory
    {
        ControllerDefinition Make(TypeInfo controllerActionKey, ControllerActionDescriptor[] actionDescriptor);
    }
}