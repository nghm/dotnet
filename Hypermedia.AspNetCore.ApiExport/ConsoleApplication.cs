namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    public class ConsoleApplication : IConsoleApplication
    {
        private readonly ILogger<ConsoleApplication> _logger;
        private readonly IAssemblyAnalyzer _analyzer;
        private readonly IPackageCompiler _compiler;
        private readonly IFileOutputLogic _fileOutputLogic;
        private readonly ApplicationOptions _options;

        public ConsoleApplication(
            IOptions<ApplicationOptions> options,
            ILogger<ConsoleApplication> logger,
            IAssemblyAnalyzer analyzer,
            IPackageCompiler compiler,
            IFileOutputLogic fileOutputLogic
        )
        {
            this._logger = logger;
            this._analyzer = analyzer;
            this._compiler = compiler;
            this._fileOutputLogic = fileOutputLogic;
            this._options = options.Value;
        }

        public void Run()
        {
            this._logger.LogInformation("Application started...");

            var path = this._options.Path;

            var result = this._analyzer.Analyze(path);

            var assemblyContent = this._compiler.Compile(result);

            this._fileOutputLogic.Save(this._options.OutputPath, assemblyContent);

            this._logger.LogInformation("Application closed...");
        }
    }
}