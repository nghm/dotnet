namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Parallel;
    using System.Threading.Tasks;

    internal class AddSourcePropertiesStep : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private object _properties;

        public void Configure(object properties)
        {
            this._properties = properties;
        }

        public Task BuildAsync(IEntityBuilder builder)
        {
            builder.WithProperties(this._properties);

            return Task.CompletedTask;
        }
    }
}