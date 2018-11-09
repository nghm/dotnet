namespace Hypermedia.AspNetCore.Siren
{
    using Entities;
    using Entities.Builder;

    public interface IHypermediaResource
    {
        void Configure(IEntityBuilder builder);
    }
}
