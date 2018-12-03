namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Newtonsoft.Json;

    [JsonConverter(typeof(FieldJsonConverter))]
    public interface IField
    {
        string Name { get; }

        object Value { get; }
    }
}