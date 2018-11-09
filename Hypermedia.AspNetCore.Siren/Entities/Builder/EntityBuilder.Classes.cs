namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    internal partial class EntityBuilder
    {
        public IEntityBuilder WithClasses(params string[] classes)
        {
            foreach (var @class in classes)
            {
                this._classes.Add(@class);
            }

            return this;
        }
    }
}