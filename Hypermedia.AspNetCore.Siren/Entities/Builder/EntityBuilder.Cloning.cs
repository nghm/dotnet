namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    internal partial class EntityBuilder
    {
        private TypedEntityBuilder<T> TypedEmptyClone<T>() where T : class
        {
            return new TypedEntityBuilder<T>(
                this._mapper,
                this._endpointDescriptorProvider,
                this._hrefFactory,
                this._fieldsFactory,
                this._claimsPrincipal);
        }

        private EntityBuilder EmptyClone()
        {
            return new EntityBuilder(
                this._mapper,
                this._endpointDescriptorProvider,
                this._hrefFactory,
                this._fieldsFactory,
                this._claimsPrincipal
            );
        }
    }
}