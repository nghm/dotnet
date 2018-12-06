namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using Builders.Abstractions;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using Util;

    internal class FieldJsonConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType) => typeof(IField).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer _)
        {
            if (!(value is Field field))
            {
                throw new InvalidCastException($"Object is not {nameof(Field)}!");
            }

            var o = new JObject
            {
                new JProperty("name", field.Name.ToCamelCase())
            };

            if (field.Value != null)
            {
                o.Add(new JProperty("value", JToken.FromObject(field.Value)));
            }

            foreach (var meta in field.GetMetadata())
            {
                if (o.ContainsKey(meta.Key)
                    || meta.Value == null)
                {
                    continue;
                }

                var valueObj = JToken.FromObject(meta.Value);

                o.Add(new JProperty(meta.Key, valueObj));
            }

            o.WriteTo(writer);
        }
    }
}