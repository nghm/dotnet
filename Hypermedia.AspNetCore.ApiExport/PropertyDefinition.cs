namespace Hypermedia.AspNetCore.ApiExport
{
    internal class PropertyDefinition
    {
        public string Name { get; set; }
        public TypeDescriptor Type { get; set; }

        public override string ToString()
        {
            return
                $"          public {this.Type.Name} {this.Name} {{ get; set; }}\n";
        }
    }
}