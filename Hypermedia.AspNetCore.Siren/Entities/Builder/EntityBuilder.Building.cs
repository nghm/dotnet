namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using System.Linq;

    internal partial class EntityBuilder
    {
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

        private void BuildOver(EntityBuilder entityBuilder)
        {
            this._classes.ToList()
                .ForEach(@class => entityBuilder._classes.Add(@class));
            this._links.ToList()
                .ForEach(link => entityBuilder._links.Add(link));
            this._entities.ToList()
                .ForEach(entity => entityBuilder._entities.Add(entity));
            this._properties.ToList()
                .ForEach(property => entityBuilder._properties.Add(property));
            this._actions.ToList()
                .ForEach(action => entityBuilder._actions.Add(action));
        }
    }
}