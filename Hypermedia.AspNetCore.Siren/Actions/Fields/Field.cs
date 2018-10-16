using System;
using System.Collections.Generic;
using System.Text;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    class Field : IField
    {
        public string Name { get; }
        public object Value { get; }
        public IEnumerable<IFieldMetadata> Metadata { get; }

        public Field(string name, object value, IEnumerable<IFieldMetadata> metadata)
        {
            Name = name;
            Value = value;
            Metadata = metadata;
        }
    }
}
