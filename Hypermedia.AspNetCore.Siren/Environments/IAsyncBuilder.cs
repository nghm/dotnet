namespace Hypermedia.AspNetCore.Siren.Environments
{
    using System.Threading.Tasks;

    public interface IAsyncBuilder<TBuilt> where TBuilt : class
    {
        Task<TBuilt> BuildAsync();
    }
}