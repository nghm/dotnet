namespace Hypermedia.AspNetCore.ApiExport.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using AssemblyAnalyzer;
    using AutoFixture.Xunit2;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes;
    using Xunit;

    public class AssemblyAnalyzerTests
    {
        [Theory]
        [AutoMockData]
        void AnalyzeIsLoggingAnalyzeStart(
            string path,
            [Frozen] Mock<ILogger<AssemblyAnalyzer>> logger,
            AssemblyAnalyzer assemblyAnalyzer
        )
        {
            assemblyAnalyzer.Analyze(path);

            logger.Verify(LogLevel.Information, $"Analyze {path} started...", Times.Once());
        }

        [Theory, AutoMockData]
        void AnalyzeIsLoadingAssemblyFromPath(
            string path,
            [Frozen] Mock<IAssemblyLoader> assemblyLoader,
            AssemblyAnalyzer assemblyAnalyzer
        )
        {
            assemblyAnalyzer.Analyze(path);

            assemblyLoader.Verify(a => a.Load(path));
        }

        [Theory, AutoMockData]
        void AnalyzeAssemblyExtractsControllerTypes(
            string path, Assembly assembly,
            [Frozen] Mock<IControllerTypeExtractor> controllerTypeExtractor,
            [Frozen] Mock<IAssemblyLoader> assemblyLoader,
            AssemblyAnalyzer assemblyAnalyzer
        )
        {
            assemblyLoader
                .Setup(a => a.Load(path))
                .Returns(assembly);

            assemblyAnalyzer.Analyze(path);

            controllerTypeExtractor
                .Verify(c => c.Extract(assembly), Times.Once());
        }

        [Theory, AutoMockData]
        void AnalyzeCreatesActionDescriptorContexts(
            string path,
            Type[] controllerTypes,
            [Frozen] Mock<IControllerTypeExtractor> controllerTypeExtractor,
            [Frozen] Mock<IActionDescriptorContextFactory> actionDescriptorContextBuilder,
            AssemblyAnalyzer assemblyAnalyzer
        )
        {
            controllerTypeExtractor
                .Setup(c => c.Extract(It.IsAny<Assembly>()))
                .Returns(controllerTypes);

            assemblyAnalyzer.Analyze(path);

            foreach (var controllerType in controllerTypes)
            {
                actionDescriptorContextBuilder
                    .Verify(a => a.Make(controllerType));
            }
        }

        [Theory, AutoMockData]
        void AnalyzeReturnsActionDescriptorContextCollection(
            string path,
            ActionDescriptorContext[] actionDescriptorContexts,
            [Frozen] Mock<IActionDescriptorContextFactory> actionDescriptorContextBuilder,
            AssemblyAnalyzer assemblyAnalyzer
        )
        {
            var queue = new Queue<ActionDescriptorContext>(actionDescriptorContexts);

            actionDescriptorContextBuilder
                .Setup(a => a.Make(It.IsAny<Type>()))
                .Returns(queue.Dequeue);

            var analysisResult = assemblyAnalyzer.Analyze(path);

            Assert.Equal(analysisResult.ActionDescriptorContexts, actionDescriptorContexts);
        }
    }
}
