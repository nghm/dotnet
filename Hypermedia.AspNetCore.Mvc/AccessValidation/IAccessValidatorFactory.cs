namespace Hypermedia.AspNetCore.Mvc.AccessValidation
{
    using Microsoft.AspNetCore.Authorization;

    internal interface IAccessValidatorFactory
    {
        IAccessValidator Make(AuthorizationPolicy[] policies);
    }
}