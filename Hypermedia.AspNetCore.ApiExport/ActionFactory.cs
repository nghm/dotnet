namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Internal;

    internal class ActionFactory : IActionFactory
    {
        private readonly IParameterFactory _parameterFactory;

        public ActionFactory(IParameterFactory parameterFactory)
        {
            this._parameterFactory = parameterFactory;
        }

        public ActionDefinition Make(ControllerActionDescriptor actionDescriptor)
        {
            return new ActionDefinition()
            {
                Name = actionDescriptor.MethodInfo.Name,
                Route = actionDescriptor.AttributeRouteInfo.Template,
                Methods = actionDescriptor
                    .ActionConstraints
                    .OfType<HttpMethodActionConstraint>()
                    .SelectMany(c => c.HttpMethods.Select(m => $"Microsoft.AspNetCore.Mvc.Http{SanitizePascal(m)}Attribute")),
                Parameters = actionDescriptor
                    .Parameters
                    .Select(p => this._parameterFactory.Make(p))
            };
        }

        private string SanitizePascal(string method)
        {
            return method.First() + method.Substring(1).ToLower();
        }
    }
}