using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    internal class ActionArgument
    {
        public object Value { get; }
        public object DefaultValue { get; }
        public string Name { get; }
        public BindingSource BindingSource { get; }
        public FieldDescriptor[] FieldDescriptors { get; }

        public ActionArgument(
            string name,
            object value,
            object defaultValue,
            BindingSource bindingSource,
            FieldDescriptor[] fieldDescriptors)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            Name = name;

            Value = value;
            DefaultValue = defaultValue;

            BindingSource =
                bindingSource ??
                throw new ArgumentNullException(nameof(bindingSource));

            FieldDescriptors =
                fieldDescriptors ??
                throw new ArgumentNullException(nameof(fieldDescriptors));
        }

        public bool ValueIsDefaultValue() => Value?.Equals(this.DefaultValue) ?? this.DefaultValue == null;
    }
}