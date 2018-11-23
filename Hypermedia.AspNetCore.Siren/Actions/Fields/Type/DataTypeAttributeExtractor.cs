namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    internal class DataTypeAttributeExtractor : IDataTypeAttributeExtractor
    {
        public DataTypeAttribute GetDataTypeAttribute(FieldGenerationContext fieldGenerationContext)
        {
            return fieldGenerationContext.FieldDescriptor.CustomAttributes
                .OfType<DataTypeAttribute>()
                .SingleOrDefault();
        }
    }
}