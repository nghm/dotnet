namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Collections.Generic;
    using System.Linq;

    internal class Field : IField
    {
        public Field(string name, object value, IEnumerable<KeyValuePair<string, object>> metadata = null)
        {
            this.Name = name;
            this.Value = value;
            this.Metadata = (metadata ?? Enumerable.Empty<KeyValuePair<string, object>>()).ToList();
        }

        public string Name { get; }
        public object Value { get; }
        public IEnumerable<KeyValuePair<string, object>> Metadata { get; }

        public void AddMetadata(KeyValuePair<string, object> meta)
        {
            var metadata = this.Metadata as List<KeyValuePair<string, object>>;

            metadata?.Add(meta);
        }
    }
}
