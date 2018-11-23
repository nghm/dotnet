using Hypermedia.AspNetCore.Siren.Util;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Linq;
using System.Reflection;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class ActionArgument
    {
        public ControllerParameterDescriptor Descriptor { get; }
        public object Value { get; }
        public object DefaultValue { get; }

        public ActionArgument(ControllerParameterDescriptor descriptor, object value)
        {
            ParameterUtils.NullCheck(descriptor, nameof(descriptor));

            Descriptor = descriptor;
            Value = value;
            DefaultValue = descriptor.ParameterInfo.DefaultValue;
        }

        public FieldDescriptor[] GetFieldDescriptors()
        {
            return Descriptor.ParameterType
                .GetProperties()
                .Where(IsField)
                .Select(p => new FieldDescriptor(
                    p.Name,
                    p.GetValue(Value),
                    p.PropertyType,
                    p.PropertyType.GetCustomAttributes(true)))
                .ToArray();
        }

        public bool ValueIsDefaultValue()
        {
            var parameterInfoDefaultValue = this.DefaultValue;

            return Equals(parameterInfoDefaultValue, this.Value);
        }

        private bool IsField(PropertyInfo p)
        {
            return p.SetMethod != null && p.SetMethod.IsPublic && !p.SetMethod.IsAbstract;
        }
    }
}