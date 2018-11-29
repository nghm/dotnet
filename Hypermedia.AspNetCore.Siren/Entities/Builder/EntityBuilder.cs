namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Actions;
    using Links;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.Linq;

    internal class EntityBuilder : IEntityBuilder
    {
        private readonly ISet<string> _classes = new HashSet<string>();
        private readonly IDictionary<string, object> _properties = new Dictionary<string, object>();
        private readonly IList<IEntity> _entities = new List<IEntity>();
        private readonly IList<ILink> _links = new List<ILink>();
        private readonly IList<IAction> _actions = new List<IAction>();

        public IEntity Build()
        {
            return new Entity(
                this._classes.ToArray(),
                this._entities.ToArray(),
                this._links.ToArray(),
                this._properties,
                this._actions.ToArray()
            );
        }

        public IEntityBuilder WithClasses(params string[] classes)
        {
            foreach (var @class in classes)
            {
                this._classes.Add(@class);
            }

            return this;
        }

        public IEntityBuilder WithAction(IAction action)
        {
            this._actions.Add(action);

            return this;
        }

        public IEntityBuilder WithEntity(IEntity entity)
        {
            this._entities.Add(entity);

            return this;
        }

        public IEntityBuilder WithLink(ILink link)
        {
            this._links.Add(link);

            return this;
        }

        public IEntityBuilder WithProperties<TProp>(TProp properties)
        {
            foreach (var property in JObject.FromObject(properties)
                .ToObject<IDictionary<string, object>>())
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