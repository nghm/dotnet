using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    internal class DataTypeAttributeExtractor : IDataTypeAttributeExtractor
    {
        public DataTypeAttribute GetDataTypeAttribute(object[] customAttributes)
        {
            if (customAttributes == null)
            {
                throw new ArgumentNullException(nameof(customAttributes));
            }

            return customAttributes.OfType<DataTypeAttribute>()
                .SingleOrDefault();
        }
    }
}