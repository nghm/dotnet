namespace Hypermedia.AspNetCore.Mvc
{
    using Microsoft.AspNetCore.Authorization;

    internal interface IAccessValidatorFactory
    {
        IAccessValidator Make(AuthorizationPolicy[] policies);
    }
}