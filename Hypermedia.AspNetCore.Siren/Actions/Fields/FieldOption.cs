﻿using Newtonsoft.Json;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldOption
    {
        [JsonProperty("value")]
        public object Value { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}