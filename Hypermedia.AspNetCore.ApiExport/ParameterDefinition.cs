namespace Hypermedia.AspNetCore.ApiExport
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    internal class ParameterDefinition
    {
        public string Name { get; set; }
        public string Binding { get; set; }
        public TypeDescriptor Type { get; set; }

        public override string ToString()
        {
            return 
                $"[From{this.Binding}] {this.Type.Name} {this.Name}";
        }
    }
}