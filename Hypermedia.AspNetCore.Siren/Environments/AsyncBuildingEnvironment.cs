namespace Hypermedia.AspNetCore.Siren.Environments
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class AsyncBuildingEnvironment<TBuilder, TBuilt>
        : IAsyncBuildingEnvironment<TBuilder, TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        private readonly IStorage<(Type ServiceType, Action<IAsyncBuildStep<TBuilder, TBuilt>> Configure)> _parts;
        private readonly TBuilder _builder;
        private readonly IScopedBuildApplier<TBuilder, TBuilt> _scopedBuildApplier;

        public AsyncBuildingEnvironment(
            IStorage<(Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>)> parts,
            TBuilder builder,
            IScopedBuildApplier<TBuilder, TBuilt> scopedBuildApplier
        )
        {
            this._parts = parts ?? throw new ArgumentNullException(nameof(parts));
            this._builder = builder ?? throw new ArgumentNullException(nameof(builder));
            this._scopedBuildApplier = scopedBuildApplier ?? throw new ArgumentNullException(nameof(builder));
        }

        public void AddAsyncBuildStep<TStep>(Action<TStep> configure)
            where TStep : class, IAsyncBuildStep<TBuilder, TBuilt>
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            this._parts.Add((typeof(TStep), step => configure(step as TStep)));
        }

        public async Task<TBuilt> BuildAsync()
        {
            await Task.WhenAll(
                this._parts.Select(p => this._scopedBuildApplier.ApplyScopedBuild(p, this._builder))
            );

            return await Task.FromResult(this._builder.Build());
        }
    }
}
