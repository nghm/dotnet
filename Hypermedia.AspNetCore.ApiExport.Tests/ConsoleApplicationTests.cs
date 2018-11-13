
namespace Hypermedia.AspNetCore.ApiExport.Tests
{
    using AssemblyAnalyzer;
    using Xunit;

    using AutoFixture.Xunit2;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;

    public class ConsoleApplicationTests
    {
        [Theory]
        [AutoMockData]
        void RunIsLoggingStart(
            [Frozen] Mock<ILogger<ConsoleApplication>> logger,
            ConsoleApplication consoleApplication)
        {
            consoleApplication.Run();

            logger.Verify(LogLevel.Information, "Application started...", Times.Once());
        }

        [Theory]
        [AutoMockData]
        void RunIsLoggingEnd(
            [Frozen] Mock<ILogger<ConsoleApplication>> logger,
            ConsoleApplication consoleApplication)
        {
            consoleApplication.Run();

            logger.Verify(LogLevel.Information, "Application closed...", Times.Once());
        }

        [Theory]
        [AutoMockData]
        void RunAnalyzesPathFromOptions(
            [Frozen] IOptions<ApplicationOptions> options,
            [Frozen] Mock<IAssemblyAnalyzer> analyzer,
            ConsoleApplication consoleApplication)
        {
            consoleApplication.Run();

            analyzer.Verify(a => a.Analyze(options.Value.Path));
        }

        [Theory]
        [AutoMockData]
        void RunCompilesAssemblyAnalysisResult(
            IAssemblyAnalysisResult result,
            [Frozen] Mock<IAssemblyAnalyzer> analyzer,
            [Frozen] Mock<IPackageCompiler> compiler,
            ConsoleApplication consoleApplication)
        {
            analyzer
                .Setup(a => a.Analyze(It.IsAny<string>()))
                .Returns(result);

            consoleApplication.Run();

            compiler.Verify(a => a.Compile(result));
        }

        [Theory]
        [AutoMockData]
        void RunStoresAssemblyAnalysisResult(
            string compilationResult,
            [Frozen] IOptions<ApplicationOptions> options,
            [Frozen] Mock<IPackageCompiler> compiler,
            [Frozen] Mock<IFileOutputLogic> fileOutputLogic,
            ConsoleApplication consoleApplication
        )
        {
            compiler
                .Setup(c => c.Compile(It.IsAny<IAssemblyAnalysisResult>()))
                .Returns(compilationResult);

            consoleApplication.Run();

            fileOutputLogic.Verify(f => f.Save(options.Value.OutputPath, compilationResult));
        }
    }
}
