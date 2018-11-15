namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    internal class ControllerDefinition
    {
        public string Name { get; set; }
        public IEnumerable<ActionDefinition> Actions { get; set; }
        public string Route { get; set; }
    }
}