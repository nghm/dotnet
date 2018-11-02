namespace Hypermedia.WebApi.Services
{
    public interface ICrudServiceSeed<T, out TKey> where T : class, IEntity<TKey>
    {
        void Seed(int amount);
    }
}
