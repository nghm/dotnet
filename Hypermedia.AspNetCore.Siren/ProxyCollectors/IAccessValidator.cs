namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    internal interface IAccessValidator
    {
        bool CanAccess(ClaimsPrincipal user, AuthorizationPolicy[] policies);
    }
}