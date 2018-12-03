using System;

namespace Hypermedia.AspNetCore.Siren.Parallel
{
    internal interface IAsyncBuildingEnvironment<TBuilder, TBuilt> : IAsyncBuilder<TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        void AddAsyncBuildStep<TStep>(Action<TStep> configure)
            where TStep : class, IAsyncBuildStep<TBuilder, TBuilt>;
    }
}