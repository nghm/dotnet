namespace Hypermedia.AspNetCore.Mvc.AccessValidation
{
    using System.Security.Claims;

    public interface IAccessValidator
    {
        bool Allows(ClaimsPrincipal user);
    }
}