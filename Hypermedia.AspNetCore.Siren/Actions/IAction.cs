namespace Hypermedia.AspNetCore.Siren.Actions
{
    using Hypermedia.AspNetCore.Siren.Actions.Fields;
    using System.Collections.Generic;

    public interface IAction
    {
        string Href { get; }
        string Method { get; }
        IReadOnlyList<IField> Fields { get; }
        string Name { get; }
    }
}