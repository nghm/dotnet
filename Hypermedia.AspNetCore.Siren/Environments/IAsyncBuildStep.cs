namespace Hypermedia.AspNetCore.Siren.Environments
{
    using System.Threading.Tasks;

    internal interface IAsyncBuildStep<in TBuilder, in TBuilt>
        where TBuilder : class, IBuilder<TBuilt>
        where TBuilt : class
    {
        Task BuildAsync(TBuilder builder);
    }
}