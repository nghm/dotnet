namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    public interface IAction
    {
        string Href { get; }
        string Method { get; }
        IFields Fields { get; }
        string Name { get; }
    }
}