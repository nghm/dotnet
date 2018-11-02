namespace Hypermedia.AspNetCore.Siren.Links
{
    class Link : ILink
    {
        public string Name { get; set; }
        public string Href { get; set; }
        public string[] Rel { get; set; }
    }
}
