namespace Hypermedia.AspNetCore.Siren.Parallel
{
    public interface IBuilder<out TBuilt> where TBuilt : class
    {
        TBuilt Build();
    }
}