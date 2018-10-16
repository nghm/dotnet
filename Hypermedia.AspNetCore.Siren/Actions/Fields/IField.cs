using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    [JsonConverter(typeof(FieldJsonConverter))]
    public interface IField
    {
        string Name { get; }
        object Value { get; }
        [JsonIgnore]
        IEnumerable<IFieldMetadata> Metadata { get; }
    }
}