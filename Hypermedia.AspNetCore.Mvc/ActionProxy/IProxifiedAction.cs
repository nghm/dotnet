namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using System.Security.Claims;

    internal interface IProxifiedAction
    {
        string Href { get; }
        bool Allows(ClaimsPrincipal user);
    }
}