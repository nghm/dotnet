namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;

    internal class ActionDefinition
    {
        public string Name { get; set; }
        public IEnumerable<ParameterDefinition> Parameters { get; set; }
        public string Route { get; set; }
        public IEnumerable<string> Methods { get; set; }
    }
}