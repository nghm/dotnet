namespace Hypermedia.AspNetCore.Mvc.AccessValidation
{
    using Microsoft.AspNetCore.Authorization;

    internal class AccessValidatorFactory : IAccessValidatorFactory
    {
        private readonly IAuthorizationService _authorization;

        public AccessValidatorFactory(IAuthorizationService authorization)
        {
            this._authorization = authorization;
        }

        public IAccessValidator Make(AuthorizationPolicy[] policies)
        {
            return new AccessValidator(this._authorization, policies);
        }
    }
}
