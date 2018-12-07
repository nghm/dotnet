

namespace Hypermedia.AspNetCore.AsyncStepBuilder
{
    using Core;
    using System;
    using System.Threading.Tasks;

    internal interface IIsolatedBuildStepExecutor<TBuilder, TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        Task ExecuteBuildStepAsync((Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>) part, TBuilder builder);
    }
}