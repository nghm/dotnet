using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class CustomAttributeDataWrapper : ReflectedTypeWrapper<CustomAttributeData>, ICustomAttributeData
    {
        public CustomAttributeDataWrapper(IReflection reflection, CustomAttributeData customAttributeData)
            : base(reflection, customAttributeData)
        { }

        public IType AttributeType
            => _reflection.TypeOf(_underlyingType.AttributeType);

        public IConstructorInfo Constructor
            => _reflection.ConstructorInfo(_underlyingType.Constructor);

        public IList<CustomAttributeTypedArgument> ConstructorArguments
            => _underlyingType.ConstructorArguments;

        public IList<CustomAttributeNamedArgument> NamedArguments
            => _underlyingType.NamedArguments;
    }
}