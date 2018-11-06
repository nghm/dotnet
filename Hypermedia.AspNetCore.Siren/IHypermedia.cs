﻿using Hypermedia.AspNetCore.Siren.Entities;
using System.Security.Claims;

namespace Hypermedia.AspNetCore.Siren
{
    public interface IHypermedia
    {
        IEntityBuilder MakeEntity();
        IEntityBuilder MakeEntity(ClaimsPrincipal user);
    }
}