using System;
using Hypermedia.AspNetCore.Store;
using Microsoft.Extensions.DependencyInjection;

namespace Hypermedia.AspNetCore.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddHypermediaStorage(this IServiceCollection serviceCollection)
        {
            if (serviceCollection == null)
            {
                throw new ArgumentNullException(nameof(serviceCollection));
            }

            serviceCollection.AddTransient(typeof(IStorage<>), typeof(Storage<>));

            return serviceCollection;
        }
    }
}