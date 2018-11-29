namespace Hypermedia.AspNetCore.Siren.Parallel
{
    using System.Threading.Tasks;

    internal interface IParallelBuildStep<in TBuilder, in TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        Task BuildAsync(TBuilder builder);
    }
}