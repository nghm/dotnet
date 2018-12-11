namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using Microsoft.AspNetCore.Authorization;

    internal class ApiActionDescriptor : IApiActionDescriptor
    {
        public ApiActionDescriptor(
            string name,
            string template,
            (string Name, int Index)[] query,
            (string Name, int Index)[] route,
            AuthorizationPolicy[] policies
        )
        {
            this.Name = name;
            this.Template = template;
            this.Query = query;
            this.Route = route;
            this.Policies = policies;
        }

        public string Name { get; }
        public string Template { get; }
        public (string Name, int Index)[] Query { get; }
        public (string Name, int Index)[] Route { get; }
        public (string Name, int Index) Body { get; }
        public AuthorizationPolicy[] Policies { get; set; }
    }
}