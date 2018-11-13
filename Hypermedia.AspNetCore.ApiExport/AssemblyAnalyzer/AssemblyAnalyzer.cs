namespace Hypermedia.AspNetCore.ApiExport.AssemblyAnalyzer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    class AssemblyAnalyzer : IAssemblyAnalyzer
    {
        private readonly ILogger<AssemblyAnalyzer> _logger;
        private readonly IAssemblyLoader _assemblyLoader;
        private readonly IControllerTypeExtractor _controllerTypeExtractor;
        private readonly IActionDescriptorContextFactory _actionDescriptorContextFactory;

        public AssemblyAnalyzer(
            ILogger<AssemblyAnalyzer> logger, 
            IAssemblyLoader assemblyLoader, 
            IControllerTypeExtractor controllerTypeExtractor, 
            IActionDescriptorContextFactory actionDescriptorContextFactory)
        {
            this._logger = logger;
            this._assemblyLoader = assemblyLoader;
            this._controllerTypeExtractor = controllerTypeExtractor;
            this._actionDescriptorContextFactory = actionDescriptorContextFactory;
        }

        public IAssemblyAnalysisResult Analyze(string path)
        {
            this._logger.LogInformation($"Analyze {path} started...");

            var assembly = this._assemblyLoader.Load(path);

            var controllerTypes = this._controllerTypeExtractor.Extract(assembly);

            var actionDescriptorContexts = controllerTypes
                .Select(controllerType => this._actionDescriptorContextFactory.Make(controllerType))
                .ToList();

            return new AssemblyAnalysisResult(actionDescriptorContexts);
        }
    }

    internal class AssemblyAnalysisResult : IAssemblyAnalysisResult
    {
        public AssemblyAnalysisResult(IEnumerable<ActionDescriptorContext> actionDescriptorContexts)
        {
            this.ActionDescriptorContexts = actionDescriptorContexts;
        }

        public IEnumerable<ActionDescriptorContext> ActionDescriptorContexts { get; }
    }
}
