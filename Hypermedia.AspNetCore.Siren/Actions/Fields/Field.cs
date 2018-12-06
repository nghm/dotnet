using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Builders.Abstractions;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    [JsonConverter(typeof(FieldJsonConverter))]
    internal class Field : IField
    {
        public string Name { get; }
        public object Value { get; }
        private readonly ICollection<KeyValuePair<string, object>> _metadata;

        public Field(string name, object value, ICollection<KeyValuePair<string, object>> metadata = null)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            this.Name = name;
            this.Value = value;
            this._metadata = metadata ?? Enumerable.Empty<KeyValuePair<string, object>>().ToList();
        }

        public void AddMetadata(KeyValuePair<string, object> meta)
        {
            if (meta.Key == null)
            {
                throw new ArgumentNullException(nameof(meta.Key));
            }

            if (string.IsNullOrWhiteSpace(meta.Key))
            {
                throw new ArgumentException(nameof(meta.Key));
            }

            this._metadata.Add(meta);
        }

        public IReadOnlyCollection<KeyValuePair<string, object>> GetMetadata() =>
            _metadata.ToList();
    }
}