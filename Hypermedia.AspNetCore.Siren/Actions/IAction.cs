using Hypermedia.AspNetCore.Siren.Actions.Fields;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions
{
    public interface IAction
    {
        string Href { get; }
        string Method { get; }
        IEnumerable<IField> Fields { get; }
        string Name { get; }
    }
}