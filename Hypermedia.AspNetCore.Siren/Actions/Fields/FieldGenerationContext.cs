using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Reflection;

    public class FieldGenerationContext
    {
        public ParameterDescriptor ParameterInfo { get; internal set; }
        public object Value { get; internal set; }
        public string Name { get; internal set; }
        public PropertyInfo PropertyInfo => this.ParameterInfo.ParameterType.GetProperty(this.Name);
    }
}