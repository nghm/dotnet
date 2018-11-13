namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Console;

    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .AddApplicationServices(args)
                .BuildServiceProvider();

            var application = serviceProvider.GetService<IConsoleApplication>();

            application.Run();
        }
    }
}
