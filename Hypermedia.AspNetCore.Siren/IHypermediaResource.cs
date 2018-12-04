namespace Hypermedia.AspNetCore.Siren
{
    using Resources;

    public interface IHypermediaResource
    {
        void Configure(IResourceBuilder builder);
    }
}
