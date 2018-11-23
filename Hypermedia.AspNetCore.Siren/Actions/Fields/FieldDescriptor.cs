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
            this.Name = name;
            this.Value = value;
            this.PropertyType = propertyType;
            this.CustomAttributes = customAttributes;
        }
    }
}