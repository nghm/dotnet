namespace Hypermedia.WebApi.Services
{
    using System.Linq;

    public static class CrudServiceExtensions
    {
        public static T[] Paginate<T, TKey>(
            this ICrudService<T, TKey> service, 
            int perPage, 
            int pageNo
        )
            where T : class, IEntity<TKey>
        {
            return service
                .All()
                .Skip(perPage * pageNo)
                .Take(perPage)
                .ToArray();
        }
    }
}
