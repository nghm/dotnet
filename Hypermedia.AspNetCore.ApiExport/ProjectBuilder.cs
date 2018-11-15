namespace Hypermedia.AspNetCore.ApiExport
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Options;

    internal class ProjectBuilder : IProjectBuilder
    {
        private readonly IOptions<ApplicationOptions> _options;

        public ProjectBuilder(IOptions<ApplicationOptions> options)
        {
            this._options = options;

            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly(this._options.Value.TempBuildDir);
        }

        public ServiceProvider Build()
        {
            var services = new ServiceCollection();
            var mvcBuilder = services
                .AddLogging()
                .AddOptions()
                .AddMvcCore();

            var process = Process.Start("dotnet", $"publish {this._options.Value.Input} -c Release -o {this._options.Value.TempBuildDir}");

            process?.WaitForExit();

            if (process?.ExitCode != 0)
            {
                Console.WriteLine("Project build failed...");
            }

            foreach (var controllerAssembly in this._options.Value.ControllerAssemblies)
            {
                var assembly = Assembly.LoadFile($"{this._options.Value.TempBuildDir}/{controllerAssembly}.dll");

                mvcBuilder.AddApplicationPart(assembly);
            }

            return services.BuildServiceProvider();
        }

        private static ResolveEventHandler ResolveAssembly(string path)
        {
            return (o, args) =>
            {
                var assemblyName = args.Name.Split(",").First();

                return Assembly.LoadFile($"{path}/{assemblyName}.dll");
            };
        }
    }
}