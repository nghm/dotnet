namespace Hypermedia.AspNetCore.Siren.Endpoints
{
    using System.Linq;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Authorization;

    internal class AccessValidator : IAccessValidator
    {
        private readonly IAuthorizationService _authorizationService;

        public AccessValidator(IAuthorizationService authorizationService)
        {
            this._authorizationService = authorizationService;
        }

        public bool CanAccess(ClaimsPrincipal user, AuthorizationPolicy[] policies)
        {
            if (policies.Length == 0)
            {
                return true;
            }

            return policies.Any(authorizationPolicy => IsAuthorized(user, authorizationPolicy));
        }

        private bool IsAuthorized(ClaimsPrincipal user, AuthorizationPolicy authorizationPolicy)
        {
            return this._authorizationService
                .AuthorizeAsync(user, authorizationPolicy)
                .Result
                .Succeeded;
        }
    }
}