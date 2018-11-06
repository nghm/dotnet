namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using Entities;

    internal interface IEntityBuilderFactory
    {
        IEntityBuilder MakeEntity();
        IEntityBuilder MakeEntity(ClaimsPrincipal user);
    }
}