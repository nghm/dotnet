namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtensionMethods
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .AddJsonFile("externalize.json", true)
                .Build();
            
           services
                .Configure<ApplicationOptions>(config)
                .AddSingleton<IExporter, Exporter>()
                .AddSingleton<ITypeFactory, TypeFactory>()
                .AddSingleton<IParameterFactory, ParameterFactory>()
                .AddSingleton<IActionFactory, ActionFactory>()
                .AddSingleton<IControllerFactory, ControllerFactory>()
                .AddSingleton<IProjectBuilder, ProjectBuilder>()
                .AddSingleton<IConsoleApplication, ConsoleApplication>();

            return services;
        }

    }
}
