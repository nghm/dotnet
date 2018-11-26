using Newtonsoft.Json;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class FieldOption
    {
        [JsonProperty("value")]
        public int Value { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        //TODO: find a better solution for this
        public override bool Equals(object obj)
        {
            if (obj is FieldOption otherOption)
            {
                return this.Name == otherOption.Name
                       && this.Value == otherOption.Value;
            }

            return base.Equals(obj);
        }
    }
}