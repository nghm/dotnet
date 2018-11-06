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
            this.Metadata = metadata ?? Enumerable.Empty<KeyValuePair<string, object>>();
        }

        public string Name { get; }
        public object Value { get; }
        public IEnumerable<KeyValuePair<string, object>> Metadata { get; }
    }
}
