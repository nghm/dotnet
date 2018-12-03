namespace Hypermedia.AspNetCore.Siren.Environments
{
    public interface IBuilder<out TBuilt> where TBuilt : class
    {
        TBuilt Build();
    }
}