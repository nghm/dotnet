﻿namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Authorization;
    using System.Linq;
    using System.Security.Claims;

    internal class AccessValidator : IAccessValidator
    {
        private readonly IAuthorizationService _authorization;
        private readonly AuthorizationPolicy[] _policies;

        public AccessValidator(IAuthorizationService authorization, AuthorizationPolicy[] policies)
        {
            this._authorization = authorization;
            this._policies = policies;
        }

        public bool CanAccess(ClaimsPrincipal user)
        {
            if (this._policies.Length == 0)
            {
                return true;
            }

            return this._policies.Any(authorizationPolicy => IsAuthorized(user, authorizationPolicy));
        }

        private bool IsAuthorized(ClaimsPrincipal user, AuthorizationPolicy authorizationPolicy)
        {
            return this._authorization
                .AuthorizeAsync(user, authorizationPolicy)
                .Result
                .Succeeded;
        }
    }
}