using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    public class FieldGenerationContext
    {
        public FieldDescriptor FieldDescriptor { get; }

        public FieldGenerationContext(FieldDescriptor fieldDescriptor)
        {
            FieldDescriptor = fieldDescriptor ?? throw new ArgumentNullException(nameof(fieldDescriptor));
        }
    }
}