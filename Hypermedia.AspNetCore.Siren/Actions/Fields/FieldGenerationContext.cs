using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Reflection;

    public class FieldGenerationContext
    {

        public FieldGenerationContext(string fieldName, object fieldValue, ParameterDescriptor bodyBodyParameterDescriptor)
        {
            this.Name = fieldName;
            this.Value = fieldValue;
            this.BodyParameterDescriptor = bodyBodyParameterDescriptor;
        }

        public ParameterDescriptor BodyParameterDescriptor { get; }
        public object Value { get; }
        public string Name { get; }
        public PropertyInfo PropertyInfo => this.BodyParameterDescriptor.ParameterType.GetProperty(this.Name);
    }
}