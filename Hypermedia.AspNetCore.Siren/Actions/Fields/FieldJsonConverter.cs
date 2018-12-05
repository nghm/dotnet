﻿namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using Util;

    internal class FieldJsonConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return typeof(IField).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is Field field))
            {
                throw new InvalidCastException("Object is not IField!");
            }

            var o = new JObject
            {
                new JProperty("name", field.Name.ToCamelCase())
            };

            if (field.Value != null)
            {
                o.Add(new JProperty("value", field.Value));
            }

            foreach (var meta in field.GetMetadata())
            {
                var valueObj = JToken.FromObject(meta.Value);

                if (meta.Key == "value" && field.Value != null)
                {
                    continue;
                }

                o.AddFirst(new JProperty(meta.Key, valueObj));
            }

            o.WriteTo(writer);
        }
    }
}