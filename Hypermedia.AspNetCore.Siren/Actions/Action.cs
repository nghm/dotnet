using Hypermedia.AspNetCore.Siren.Actions.Fields;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions
{
    internal class Action : IAction
    {
        public string Href { get; set; }
        public string Method { get; set; }
        public IEnumerable<IField> Fields { get; set; }
        public string Name { get; set; }
    }
}