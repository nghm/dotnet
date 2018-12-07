namespace Hypermedia.AspNetCore.Mvc
{
    using System.Security.Claims;

    internal interface IAccessValidator
    {
        bool CanAccess(ClaimsPrincipal user);
    }
}