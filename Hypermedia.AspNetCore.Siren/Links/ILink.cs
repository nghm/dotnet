namespace Hypermedia.AspNetCore.Siren.Links
{
    internal interface ILink
    {
        string Name { get; }
        string Href { get; }
        string[] Rel { get; }
    }
}