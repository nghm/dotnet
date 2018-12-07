namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using System.Linq;

    internal class ApiActionDescriptor : IApiActionDescriptor
    {
        internal static ApiActionDescriptor From(string template, (string, int)[] query, (string, int)[] route)
        {
            return new ApiActionDescriptor(template, query, route);
        }

        public static ApiActionDescriptor From(ControllerActionDescriptor descriptor)
        {
            var parameters = descriptor.Parameters.OfType<ControllerParameterDescriptor>().ToArray();

            var query = parameters
                .Where(p => p.BindingInfo.BindingSource == BindingSource.Query)
                .Select(p => (p.Name, p.ParameterInfo.Position))
                .ToArray();
            var route = parameters
                .Where(p => p.BindingInfo.BindingSource == BindingSource.Path)
                .Select(p => (p.Name, p.ParameterInfo.Position))
                .ToArray();

            return From(descriptor.AttributeRouteInfo.Template, query, route);
        }

        private ApiActionDescriptor(string template, (string Name, int Index)[] query, (string Name, int Index)[] route)
        {
            this.Template = template;
            this.Query = query;
            this.Route = route;
        }

        public string Template { get; }
        public (string Name, int Index)[] Query { get; }
        public (string Name, int Index)[] Route { get; }
    }
}