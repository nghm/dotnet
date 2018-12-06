namespace Hypermedia.AspNetCore.Siren.Actions
{
    using Builders.Abstractions;

    internal class Action : IAction
    {
        public Action(string name, string href, string method, IFields fields = null)
        {
            this.Href = href;
            this.Method = method;
            this.Name = name;
            this.Fields = fields;
        }

        public string Href { get; }
        public string Method { get; }
        public IFields Fields { get; }
        public string Name { get; }
    }
}