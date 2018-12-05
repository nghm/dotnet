namespace Hypermedia.AspNetCore.Builder
{
    using System.Threading.Tasks;

    public interface IAsyncBuilder<TBuilt> where TBuilt : class
    {
        Task<TBuilt> BuildAsync();
    }
}