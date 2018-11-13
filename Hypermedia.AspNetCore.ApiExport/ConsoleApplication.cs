namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class ConsoleApplication : IConsoleApplication
    {
        private readonly ILogger<ConsoleApplication> _logger;
        private readonly IAssemblyAnalyzer _analyzer;
        private readonly IPackageCompiler _compiler;
        private readonly ApplicationOptions _options;

        public ConsoleApplication(
            IOptions<ApplicationOptions> options,
            ILogger<ConsoleApplication> logger,
            IAssemblyAnalyzer analyzer,
            IPackageCompiler compiler
        )
        {
            this._logger = logger;
            this._analyzer = analyzer;
            this._compiler = compiler;
            this._options = options.Value;
        }

        public void Run()
        {
            this._logger.LogInformation("Application started...");

            var path = this._options.Path;

            var result = this._analyzer.Analyze(path);

            var assemblyContent = this._compiler.Compile(result);
        }
    }
}