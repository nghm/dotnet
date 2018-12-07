namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    internal class ApiActionDescriptor : IApiActionDescriptor
    {
        public ApiActionDescriptor(string name, string template, (string Name, int Index)[] query, (string Name, int Index)[] route)
        {
            this.Name = name;
            this.Template = template;
            this.Query = query;
            this.Route = route;
        }

        public string Name { get; }
        public string Template { get; }
        public (string Name, int Index)[] Query { get; }
        public (string Name, int Index)[] Route { get; }
    }
}