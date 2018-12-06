using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    public class FieldDescriptor
    {
        public string Name { get; }
        public System.Type PropertyType { get; }
        public object Value { get; }
        public object[] CustomAttributes { get; }

        public FieldDescriptor(string name, object value, System.Type propertyType, object[] customAttributes)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            this.Name = name;
            this.Value = value;

            this.PropertyType =
                propertyType ??
                throw new ArgumentNullException(nameof(propertyType));

            this.CustomAttributes =
                customAttributes ??
                throw new ArgumentNullException(nameof(customAttributes));
        }
    }
}