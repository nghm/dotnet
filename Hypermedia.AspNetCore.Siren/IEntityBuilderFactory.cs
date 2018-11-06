namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using Entities;

    internal interface IEntityBuilderFactory
    {
        EntityBuilder MakeEntity();
        EntityBuilder MakeEntity(ClaimsPrincipal user);
    }
}