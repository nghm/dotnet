namespace Hypermedia.AspNetCore.Mvc.AccessValidation
{
    using Microsoft.Extensions.DependencyInjection;

    internal static class AccessValidationModule
    {
        public static IServiceCollection AddAccessValidationModule(this IServiceCollection services)
        {
            services.AddSingleton<IAccessValidatorFactory, AccessValidatorFactory>();

            return services;
        }
    }
}
