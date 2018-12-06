namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    using Newtonsoft.Json;

    public interface IEntity
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string Href { get; }
        [JsonProperty("class", NullValueHandling = NullValueHandling.Ignore)]
        IClasses Classes { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IEntity[] Entities { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ILink[] Links { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IProperties Properties { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IAction[] Actions { get; }
    }
}