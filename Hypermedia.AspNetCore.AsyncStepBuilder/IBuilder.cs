namespace Hypermedia.AspNetCore.Builder
{
    public interface IBuilder<out TBuilt> where TBuilt : class
    {
        TBuilt Build();
    }
}