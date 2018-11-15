namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;

    class ControllerFactory : IControllerFactory
    {
        private readonly IActionFactory _actionFactory;

        public ControllerFactory(IActionFactory actionFactory)
        {
            this._actionFactory = actionFactory;
        }

        public ControllerDefinition Make(
            TypeInfo controllerType,
            ControllerActionDescriptor[] actionDescriptor)
        {
            return new ControllerDefinition()
            {
                Name = controllerType.Name,
                Route = controllerType.GetCustomAttribute<RouteAttribute>()?.Template,
                Actions = actionDescriptor.Select(ad => this._actionFactory.Make(ad))
            };
        }
    }
}