namespace Hypermedia.AspNetCore.Siren
{
    using Entities;

    public interface IHypermediaResource
    {
        void Configure(IEntityBuilder builder);
    }
}
