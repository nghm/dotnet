namespace Hypermedia.AspNetCore.Mvc.ApiExploration
{
    using Microsoft.AspNetCore.Authorization;

    public interface IApiActionDescriptor
    {
        string Template { get; }
        (string Name, int Index)[] Query { get; }
        (string Name, int Index)[] Route { get; }
        AuthorizationPolicy[] Policies { get; set; }
    }
}