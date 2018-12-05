namespace Hypermedia.AspNetCore.Builder
{
    using Store;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class AsyncStepBuilder<TBuilder, TBuilt> : IAsyncStepBuilder<TBuilder, TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        private readonly IStorage<(Type ServiceType, Action<IAsyncBuildStep<TBuilder, TBuilt>> Configure)> _parts;
        private readonly TBuilder _builder;
        private readonly IIsolatedBuildStepExecutor<TBuilder, TBuilt> _isolatedBuildStepExecutor;

        public AsyncStepBuilder(
            IStorage<(Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>)> parts,
            TBuilder builder,
            IIsolatedBuildStepExecutor<TBuilder, TBuilt> isolatedBuildStepExecutor
        )
        {
            this._parts = parts ?? throw new ArgumentNullException(nameof(parts));
            this._builder = builder ?? throw new ArgumentNullException(nameof(builder));
            this._isolatedBuildStepExecutor = isolatedBuildStepExecutor ?? throw new ArgumentNullException(nameof(isolatedBuildStepExecutor));
        }

        public void AddStep<TStep>(Action<TStep> configure)
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
                this._parts.Select(p => this._isolatedBuildStepExecutor.ExecuteBuildStepAsync(p, this._builder))
            );

            return await Task.FromResult(this._builder.Build());
        }
    }
}
