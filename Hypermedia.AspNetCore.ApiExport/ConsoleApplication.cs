namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.Internal;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;

    internal class ConsoleApplication : IConsoleApplication
    {
        private readonly ILogger<ConsoleApplication> _logger;
        private readonly IProjectBuilder _builder;
        private readonly IControllerFactory _controllerFactory;
        private readonly IActionFactory _actionFactory;
        private readonly IExporter _exporter;
        private readonly IOptions<ApplicationOptions> _options;

        public ConsoleApplication(
            ILogger<ConsoleApplication> logger,
            IProjectBuilder builder,
            IControllerFactory controllerFactory,
            IActionFactory actionFactory, 
            IExporter exporter, 
            IOptions<ApplicationOptions> options)
        {
            this._logger = logger;
            this._builder = builder;
            this._controllerFactory = controllerFactory;
            this._actionFactory = actionFactory;
            this._exporter = exporter;
            this._options = options;
        }

        public void Run()
        {

            this._logger.LogInformation("Application started...");

            var mvc = this._builder.Build();

            var collectionProvider = mvc.GetService<IActionDescriptorCollectionProvider>();

            var descriptors = collectionProvider.ActionDescriptors.Items.ToArray();

            var controllers = new List<ControllerDefinition>();
            
            foreach (var controllerType in descriptors
                .OfType<ControllerActionDescriptor>()
                .GroupBy(c => c.ControllerTypeInfo))
            {
                var controllerDefinition = this._controllerFactory
                    .Make(controllerType.Key, controllerType.ToArray());

                controllers.Add(controllerDefinition);
            }

            var exportDefinition = new ExportDefinition
            {
                Controllers = controllers
            };

            this._exporter.Export(exportDefinition);

            this._logger.LogInformation("Application closed...");
        }
    }
}