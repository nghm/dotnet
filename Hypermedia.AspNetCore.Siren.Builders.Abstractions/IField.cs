namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    using Newtonsoft.Json;

    public interface IField
    {
        [JsonProperty("name")]
        string Name { get; }

        [JsonProperty("value")]
        object Value { get; }

        [JsonProperty("type")]
        FieldType Type { get; }
    }
}