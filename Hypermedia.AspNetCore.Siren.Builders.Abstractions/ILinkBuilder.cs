namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    using Builder;

    public interface ILinkBuilder : IBuilder<ILink>
    {
        ILinkBuilder WithName(string name);
        ILinkBuilder WithHref(string href);
        ILinkBuilder WithRel(string[] rel);
    }
}
