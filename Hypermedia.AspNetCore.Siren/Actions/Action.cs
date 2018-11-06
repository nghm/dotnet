namespace Hypermedia.AspNetCore.Siren.Actions
{
    using System;
    using Fields;
    using System.Collections.Generic;

    internal class Action : IAction
    {
        public Action(string name, string href, string method, IEnumerable<IField> fields = null)
        {
            this.Href = href;
            this.Method = method;
            this.Name = name;
            this.Fields = fields ?? Array.Empty<IField>();
        }

        public string Href { get; set; }
        public string Method { get; set; }
        public IEnumerable<IField> Fields { get; set; }
        public string Name { get; set; }
    }
}