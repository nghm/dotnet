namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using System.Security.Claims;
    using ApiExploration;

    internal interface IProxifiedAction
    {
        string Href { get; }
        IFieldDescriptor[] FieldDescriptors { get; }
        bool Allows(ClaimsPrincipal user);
    }
}