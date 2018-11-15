namespace Hypermedia.AspNetCore.ApiExport
{
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore.Internal;

    internal class TypeDescriptor
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IEnumerable<PropertyDefinition> Properties { get; set; }
        public override string ToString()
        {
            return
                  $"        public class {this.Name} {{\n" +
                        this.Properties
                            .Select(p => p.ToString())
                            .Join("\n") +
                  $"        }}\n";
        }
    }
}