namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json.Linq;

    internal partial class EntityBuilder
    {
        public IEntityBuilder WithProperties<TProp>(TProp properties)
        {
            var propDictionary = JObject
                .FromObject(properties)
                .ToObject<IDictionary<string, object>>();

            return WithProperties(propDictionary.AsEnumerable());
        }

        public IEntityBuilder WithProperties<TProp, TSource>(TSource properties)
        {
            WithProperties(this._mapper.Map<TProp>(properties));

            return this;
        }

        private IEntityBuilder WithProperties(IEnumerable<KeyValuePair<string, object>> properties)
        {
            foreach (var property in properties)
            {
                if (!this._properties.ContainsKey(property.Key))
                {
                    this._properties.Add(property.Key, property.Value);
                }
                else
                {
                    this._properties[property.Key] = property.Value;
                }
            }

            return this;
        }
    }
}