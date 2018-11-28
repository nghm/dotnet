namespace Hypermedia.AspNetCore.Siren.Links
{
    public interface ILink
    {
        string Name { get; }
        string Href { get; }
        string[] Rel { get; }
    }
}