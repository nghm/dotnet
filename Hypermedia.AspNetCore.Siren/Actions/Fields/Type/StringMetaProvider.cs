using System;

namespace Hypermedia.AspNetCore.Siren.Actions.Fields.Type
{
    using System.Collections.Generic;
    using Util;

    internal class StringMetaProvider : ITypeMetaProvider
    {
        private readonly IStringPropertyTypeMap _stringPropertyTypeMap;
        private readonly ITypeCodeExtractor _typeCodeExtractor;
        private readonly IDataTypeAttributeExtractor _dataTypeAttributeExtractor;

        public StringMetaProvider(IStringPropertyTypeMap stringPropertyTypeMap, ITypeCodeExtractor typeCodeExtractor, IDataTypeAttributeExtractor dataTypeAttributeExtractor)
        {
            Guard.EnsureIsNotNull(stringPropertyTypeMap, nameof(stringPropertyTypeMap));
            Guard.EnsureIsNotNull(typeCodeExtractor, nameof(typeCodeExtractor));
            Guard.EnsureIsNotNull(dataTypeAttributeExtractor, nameof(dataTypeAttributeExtractor));

            _stringPropertyTypeMap = stringPropertyTypeMap;
            _typeCodeExtractor = typeCodeExtractor;
            _dataTypeAttributeExtractor = dataTypeAttributeExtractor;
        }

        public IEnumerable<KeyValuePair<string, object>> GetMetadata(FieldGenerationContext fieldGenerationContext)
        {
            Guard.EnsureIsNotNull(fieldGenerationContext, nameof(fieldGenerationContext));

            var propertyType = fieldGenerationContext.FieldDescriptor.PropertyType;
            var typeCode = this._typeCodeExtractor.GetTypeCode(propertyType);

            if (typeCode != TypeCode.String)
            {
                yield break;
            }

            var type = "text";
            var dataType = _dataTypeAttributeExtractor.GetDataTypeAttribute(fieldGenerationContext);

            if (dataType != null)
            {
                type = this._stringPropertyTypeMap.MapDataType(dataType.DataType);
            }

            yield return KeyValuePair.Create("type", (object)type);
        }
    }
}