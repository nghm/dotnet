namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    internal interface IApiActionDescriptor
    {
        string Template { get; }
        (string Name, int Index)[] Query { get; }
        (string Name, int Index)[] Route { get; }
    }
}