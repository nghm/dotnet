using Microsoft.Extensions.DependencyInjection;

namespace Hypermedia.AspNetCore.Store
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypermediaStorage(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient(typeof(IStorage<>), typeof(Storage<>));

            return serviceCollection;
        }
    }
}