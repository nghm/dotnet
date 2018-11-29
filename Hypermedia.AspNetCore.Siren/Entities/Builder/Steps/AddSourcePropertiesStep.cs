namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Parallel;
    using System.Threading.Tasks;

    internal class AddSourcePropertiesStep<TProps> : IParallelBuildStep<IEntityBuilder, IEntity>
    {
        private TProps _properties;

        public void Configure(TProps properties)
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