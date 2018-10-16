using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    public class FieldGenerationContext
    {
        public ParameterDescriptor ParamterInfo { get; internal set; }
        public object Value { get; internal set; }
        public string Name { get; internal set; }
    }
}