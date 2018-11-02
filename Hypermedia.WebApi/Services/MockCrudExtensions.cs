namespace Hypermedia.WebApi.Services
{
    using System.Collections.Generic;
    using System.Linq;

    public static class MockCrudExtensions
    {
        public static T[] Paginate<T>(this MockCrudService<T> service, int perPage, int pageNo)
            where T : class 
        {
            return service
                .All()
                .Skip(perPage * pageNo)
                .Take(perPage)
                .ToArray();
        }
    }
}
