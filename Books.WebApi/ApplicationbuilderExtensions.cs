namespace Books.WebApi
{
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Builder;

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
