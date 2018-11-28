namespace Hypermedia.AspNetCore.Siren.Parallel
{
    using Entities.Builder;
    using System.Threading.Tasks;

    internal interface IEntityBuildPart
    {
        Task BuildAsync(IEntityBuilder builder);
    }
}