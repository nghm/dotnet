namespace Hypermedia.AspNetCore.Mvc
{
    internal interface IApiActionDescriptor
    {
        string Template { get; }
        (string Name, int Index)[] Query { get; }
        (string Name, int Index)[] Route { get; }
    }
}