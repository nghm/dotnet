namespace Hypermedia.AspNetCore.Siren
{
    public interface IPartialResource
    {
        IHypermediaResource PartialResource { get; }
    }
}