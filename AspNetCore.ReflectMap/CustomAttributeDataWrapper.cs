using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class CustomAttributeDataWrapper : ICustomAttributeData
    {
        private readonly CustomAttributeData _customAttributeData;

        public CustomAttributeDataWrapper(CustomAttributeData customAttributeData)
        {
            _customAttributeData = customAttributeData;
        }

        public IType AttributeType => new TypeWrapper(_customAttributeData.AttributeType);
        public IConstructorInfo Constructor => new ConstructorInfoWrapper(_customAttributeData.Constructor);
        public IList<CustomAttributeTypedArgument> ConstructorArguments => _customAttributeData.ConstructorArguments;
        public IList<CustomAttributeNamedArgument> NamedArguments => _customAttributeData.NamedArguments;
    }
}