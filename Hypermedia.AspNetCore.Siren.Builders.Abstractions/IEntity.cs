namespace Hypermedia.AspNetCore.Siren.Builders.Abstractions
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public interface IEntity
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string Href { get; }
        [JsonProperty("class", NullValueHandling = NullValueHandling.Ignore)]
        string[] Classes { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IEntity[] Entities { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        ILink[] Links { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IDictionary<string, object> Properties { get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        IAction[] Actions { get; }
    }
}