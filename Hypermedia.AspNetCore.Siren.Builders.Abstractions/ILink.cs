namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    public interface ILink
    {
        string Name { get; }
        string Href { get; }
        string[] Rel { get; }
    }
}