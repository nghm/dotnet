using Hypermedia.AspNetCore.AsyncStepBuilder;

namespace Hypermedia.AspNetCore.Siren.Resources
{
    using Hypermedia.AspNetCore.Siren.Entities;
    using System;
    using System.Threading.Tasks;

    internal class AddEmbeddedEntityStep : IAsyncBuildStep<IEntityBuilder, IEntity>
    {
        private readonly IResourceBuilder _scopedBuilder;
        private Action<IResourceBuilder> _configureBuilder;

        public AddEmbeddedEntityStep(IResourceBuilder scopedBuilder)
        {
            this._scopedBuilder = scopedBuilder ?? throw new ArgumentNullException(nameof(scopedBuilder));
        }

        public void Configure(Action<IResourceBuilder> configureBuilder)
        {
            this._configureBuilder = configureBuilder ?? throw new ArgumentNullException(nameof(configureBuilder));
        }

        public async Task BuildAsync(IEntityBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            this._configureBuilder(this._scopedBuilder);

            builder.WithEntity(await this._scopedBuilder.BuildAsync());
        }
    }
}