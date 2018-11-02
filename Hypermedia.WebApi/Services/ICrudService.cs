namespace Hypermedia.WebApi.Services
{
    using System.Linq;

    public interface ICrudService<T, in TKey> where T : class, IEntity<TKey>
    {
        IQueryable<T> All();
        void Update(TKey id, T entity);
        void Create(T entity);
        T One(TKey id);
        int Count();
    }
}