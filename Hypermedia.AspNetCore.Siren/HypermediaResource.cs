namespace Hypermedia.AspNetCore.Siren
{
    using Entities;

    public interface HypermediaResource
    {
        void Configure(IEntityBuilder builder);
    }
}
