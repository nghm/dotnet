namespace Hypermedia.AspNetCore.Siren.Actions
{
    using Builders.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class Action : IAction
    {
        public Action(string name, string href, string method, IEnumerable<IField> fields = null)
        {
            this.Href = href;
            this.Method = method;
            this.Name = name;
            this.Fields = (fields ?? Array.Empty<IField>()).ToList();
        }

        public string Href { get; }
        public string Method { get; }
        public IReadOnlyList<IField> Fields { get; }
        public string Name { get; }
    }
}