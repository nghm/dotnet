namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Environments;
    using System;
    using System.Threading.Tasks;

    internal class AddEmbeddedEntityStep : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private readonly IResourceBuilder _scopedBuilder;
        private Action<IResourceBuilder> _configureBuilder;

        public AddEmbeddedEntityStep(IResourceBuilder scopedBuilder)
        {
            this._scopedBuilder = scopedBuilder;
        }

        public void Configure(Action<IResourceBuilder> configureBuilder)
        {
            this._configureBuilder = configureBuilder;
        }

        public async Task BuildAsync(IEntityBuilder builder)
        {
            this._configureBuilder(this._scopedBuilder);

            builder.WithEntity(await this._scopedBuilder.BuildAsync());
        }
    }
}