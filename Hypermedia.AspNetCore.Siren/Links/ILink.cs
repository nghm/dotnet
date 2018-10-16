namespace Hypermedia.AspNetCore.Siren.Links
{
    public interface ILink
    {
        string Href { get; }
        string[] Rel { get; }
    }
}