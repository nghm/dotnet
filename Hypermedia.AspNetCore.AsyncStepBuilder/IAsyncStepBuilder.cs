using Hypermedia.AspNetCore.Core;

namespace Hypermedia.AspNetCore.AsyncStepBuilder
{
    using System;

    public interface IAsyncStepBuilder<TBuilder, TBuilt> : IAsyncBuilder<TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        void AddStep<TStep>(Action<TStep> configure) where TStep : class, IAsyncBuildStep<TBuilder, TBuilt>;
    }
}