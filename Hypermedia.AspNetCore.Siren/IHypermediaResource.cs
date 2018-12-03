﻿namespace Hypermedia.AspNetCore.Siren
{
    using Entities.Builder;

    public interface IHypermediaResource
    {
        void Configure(IResourceBuilder builder);
    }
}
