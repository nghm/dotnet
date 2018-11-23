using Hypermedia.AspNetCore.Siren.Util;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields
{
    public class FieldGenerationContext
    {
        public FieldDescriptor FieldDescriptor { get; }

        public FieldGenerationContext(FieldDescriptor fieldDescriptor)
        {
            Guard.EnsureIsNotNull(fieldDescriptor, nameof(fieldDescriptor));

            FieldDescriptor = fieldDescriptor;
        }
    }
}