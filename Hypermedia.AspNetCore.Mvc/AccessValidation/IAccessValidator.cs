namespace Hypermedia.AspNetCore.Mvc
{
    using System.Security.Claims;

    public interface IAccessValidator
    {
        bool CanAccess(ClaimsPrincipal user);
    }
}