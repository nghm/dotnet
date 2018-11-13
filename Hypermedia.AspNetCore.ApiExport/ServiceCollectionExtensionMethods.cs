namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string[] args)
        {
            services.AddSingleton<IConsoleApplication, ConsoleApplication>();

            return services;
        }
    }
}
