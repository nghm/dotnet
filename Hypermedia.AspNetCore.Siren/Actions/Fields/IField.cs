using Newtonsoft.Json;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    [JsonConverter(typeof(FieldJsonConverter))]
    public interface IField
    {
        string Name { get; }
        object Value { get; }
    }
}