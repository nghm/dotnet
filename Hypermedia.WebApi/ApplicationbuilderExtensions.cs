namespace Hypermedia.WebApi
{
    using Microsoft.AspNetCore.Builder;
    using Services;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMockSeeds<T, TKey>(this IApplicationBuilder builder, int amount)
            where T: class, IEntity<TKey>
        {
            var seeder = builder
                .ApplicationServices
                .GetService(typeof(ICrudServiceSeed<T, TKey>))
                as ICrudServiceSeed<T, TKey>;

            seeder?.Seed(amount);

            return builder;
        }
    }
}
