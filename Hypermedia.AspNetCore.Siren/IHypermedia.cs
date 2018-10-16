using Hypermedia.AspNetCore.Siren.Entities;
using System.Security.Claims;

namespace Hypermedia.AspNetCore.Siren
{
    public interface IHypermedia
    {
        IEntityBuilder Make(ClaimsPrincipal user);
        IEntityBuilder Make();
    }
}