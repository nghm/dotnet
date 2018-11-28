namespace Hypermedia.AspNetCore.Siren.Parallel
{
    using Entities;
    using Entities.Builder;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    internal class ParallelEntityBuilder
    {
        private readonly IEntityPartStorage _parts;
        private readonly IEntityBuilder _builder;

        public ParallelEntityBuilder(IEntityPartStorage parts, IEntityBuilder builder)
        {
            this._parts = parts ?? throw new ArgumentNullException(nameof(parts));
            this._builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        internal void AddBuildPart(IEntityBuildPart entityBuildPart)
        {
            if (entityBuildPart == null)
            {
                throw new ArgumentNullException(nameof(entityBuildPart));
            }

            this._parts.Add(entityBuildPart);
        }

        internal async Task<IEntity> BuildAsync()
        {
            await Task.WhenAll(this._parts.Select(part => part.BuildAsync(this._builder)));

            return await Task.FromResult(this._builder.Build());
        }
    }
}
