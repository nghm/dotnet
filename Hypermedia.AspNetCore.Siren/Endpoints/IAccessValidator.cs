namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System;
    using Microsoft.AspNetCore.Authorization;
    using System.Security.Claims;

    [Obsolete]
    internal interface IAccessValidator
    {
        bool CanAccess(ClaimsPrincipal user, AuthorizationPolicy[] policies);
    }
}