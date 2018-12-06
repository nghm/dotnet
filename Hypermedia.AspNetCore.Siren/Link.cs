namespace Hypermedia.AspNetCore.Siren.Links
{
    using Builders.Abstractions;
    using System;

    internal class Link : ILink
    {
        public Link(string name, string href, string[] rel = null)
        {
            this.Name = name;
            this.Href = href;
            this.Rel = rel ?? Array.Empty<string>();
        }

        public string Name { get; }
        public string Href { get; }
        public string[] Rel { get; }
    }
}
