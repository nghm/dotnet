namespace Hypermedia.AspNetCore.Siren.Entities
{
    using Builders.Abstractions;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    internal class Properties : Dictionary<string, IConvertible>, IProperties
    {
        public Properties(object properties) : base(
            JObject.FromObject(properties).ToObject<IDictionary<string, IConvertible>>()
        )
        {
        }
    }
}