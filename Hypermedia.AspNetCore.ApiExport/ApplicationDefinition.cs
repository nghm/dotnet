namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;

    internal class ApplicationDefinition
    {
        public List<ControllerDefinition> Controllers { get; set; }
        public string Name { get; set; }
    }
}