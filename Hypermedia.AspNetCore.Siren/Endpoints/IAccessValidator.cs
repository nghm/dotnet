namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    internal interface IAccessValidator
    {
        bool CanAccess(ClaimsPrincipal user, AuthorizationPolicy[] policies);
    }
}