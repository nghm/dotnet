namespace Hypermedia.AspNetCore.Siren.Entities.Builder
{
    using Parallel;
    using System;
    using System.Threading.Tasks;

    internal class AddEmbeddedEntityStep : IParallelBuildStep<IEntityBuilder, IEntity>
    {
        private readonly IApiAwareEntityBuilder _scopedBuilder;
        private Action<IApiAwareEntityBuilder> _configureBuilder;

        public AddEmbeddedEntityStep(IApiAwareEntityBuilder scopedBuilder)
        {
            this._scopedBuilder = scopedBuilder;
        }

        public void Configure(Action<IApiAwareEntityBuilder> configureBuilder)
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