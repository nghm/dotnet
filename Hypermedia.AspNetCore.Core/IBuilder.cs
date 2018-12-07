namespace Hypermedia.AspNetCore.Core
{
    public interface IBuilder<out TBuilt> where TBuilt : class
    {
        TBuilt Build();
    }
}