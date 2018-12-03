namespace Hypermedia.AspNetCore.Siren.Environments
{
    using System;
    using System.Threading.Tasks;

    internal interface IScopedBuildApplier<TBuilder, TBuilt> where TBuilder : class, IBuilder<TBuilt> where TBuilt : class
    {
        Task ApplyScopedBuild((Type, Action<IAsyncBuildStep<TBuilder, TBuilt>>) part, TBuilder builder);
    }
}