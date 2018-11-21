using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    using System.Reflection;

    public class FieldGenerationContext
    {
        public FieldGenerationContext(object fieldValue, PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            this.Value = fieldValue;
            this.PropertyInfo = propertyInfo;
        }

        public object Value { get; }

        public PropertyInfo PropertyInfo { get; }
    }
}