using System;
using System.Collections.Generic;
using System.Linq;
using Hypermedia.AspNetCore.Siren.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IField).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var field = value as IField;

            JObject o = new JObject();

            o.Add(new JProperty("name", field.Name.ToCamelCase()));

            if (field.Value != null)
            {
                o.Add(new JProperty("value", field.Value));
            }

            foreach (var meta in field.Metadata.SelectMany(meta => meta.GetMetadata()))
            {
                o.AddFirst(new JProperty(meta.Key, meta.Value ));
            }

            o.WriteTo(writer);
        }
    }
}