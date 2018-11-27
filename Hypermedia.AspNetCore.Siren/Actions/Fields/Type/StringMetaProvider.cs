using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.Collections.Generic;

    internal class StringMetaProvider : ITypeMetaProvider
    {
        private readonly IStringPropertyTypeMap _stringPropertyTypeMap;
        private readonly ITypeCodeExtractor _typeCodeExtractor;
        private readonly IDataTypeAttributeExtractor _dataTypeAttributeExtractor;

        public StringMetaProvider(
            IStringPropertyTypeMap stringPropertyTypeMap,
            ITypeCodeExtractor typeCodeExtractor,
            IDataTypeAttributeExtractor dataTypeAttributeExtractor)
        {
            _stringPropertyTypeMap =
                stringPropertyTypeMap ??
                throw new ArgumentNullException(nameof(stringPropertyTypeMap));

            _typeCodeExtractor =
                typeCodeExtractor ??
                throw new ArgumentNullException(nameof(typeCodeExtractor));

            _dataTypeAttributeExtractor =
                dataTypeAttributeExtractor ??
                throw new ArgumentNullException(nameof(dataTypeAttributeExtractor));
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            if (fieldGenerationContext == null)
            {
                throw new ArgumentNullException(nameof(fieldGenerationContext));
            }

            var propertyType = fieldGenerationContext.FieldDescriptor.PropertyType;
            var typeCode = this._typeCodeExtractor.GetTypeCode(propertyType);

            if (typeCode != TypeCode.String)
            {
                yield break;
            }

            var type = "text";
            var fieldCustomAttributes = fieldGenerationContext.FieldDescriptor.CustomAttributes;
            var dataType = _dataTypeAttributeExtractor.GetDataTypeAttribute(fieldCustomAttributes);

            if (dataType != null)
            {
                type = this._stringPropertyTypeMap.MapDataType(dataType.DataType);
            }

            yield return KeyValuePair.Create("type", (object)type);
        }
    }
}