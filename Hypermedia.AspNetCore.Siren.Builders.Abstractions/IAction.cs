namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    using System.Collections.Generic;

    public interface IAction
    {
        string Href { get; }
        string Method { get; }
        IReadOnlyList<IField> Fields { get; }
        string Name { get; }
    }
}