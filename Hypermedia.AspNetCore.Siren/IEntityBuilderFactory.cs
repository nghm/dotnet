namespace Hypermedia.AspNetCore.Siren
{
    using System.Security.Claims;
    using Entities;
    using Entities.Builder;

    internal interface IEntityBuilderFactory
    {
        EntityBuilder MakeEntity();
        EntityBuilder MakeEntity(ClaimsPrincipal user);
    }
}