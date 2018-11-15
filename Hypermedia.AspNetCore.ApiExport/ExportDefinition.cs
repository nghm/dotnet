namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;

    internal class ExportDefinition
    {
        public IEnumerable<ControllerDefinition> Controllers { get; set; }
        public string Name { get; set; }
    }
}