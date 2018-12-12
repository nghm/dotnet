using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class MemberInfoWrapper : ReflectedTypeWrapper<MemberInfo>, IMemberInfo
    {
        public MemberInfoWrapper(IReflection reflection, MemberInfo memberInfo)
            : base(reflection, memberInfo)
        { }

        public MemberTypes MemberType
            => _underlyingType.MemberType;

        public string Name
            => _underlyingType.Name;

        public IType DeclaringType
            => _reflection.TypeOf(_underlyingType.DeclaringType);

        public IType ReflectedType
            => _reflection.TypeOf(_underlyingType.ReflectedType);

        public Module Module
            => _underlyingType.Module;

        public IEnumerable<ICustomAttributeData> CustomAttributes
            => _underlyingType.CustomAttributes.Select(attribute => _reflection.CustomAttributeData(attribute));

        public int MetadataToken
            => _underlyingType.MetadataToken;

        public bool HasSameMetadataDefinitionAs(IMemberInfo other)
            => _underlyingType.HasSameMetadataDefinitionAs(other.GetUnderlyingType());

        public bool IsDefined(IType attributeType, bool inherit)
            => _underlyingType.IsDefined(attributeType.GetUnderlyingType(), inherit);

        public object[] GetCustomAttributes(bool inherit)
            => _underlyingType.GetCustomAttributes(inherit);

        public object[] GetCustomAttributes(IType attributeType, bool inherit)
            => _underlyingType.GetCustomAttributes(attributeType.GetUnderlyingType(), inherit);

        public IList<ICustomAttributeData> GetCustomAttributesData()
            => _underlyingType.GetCustomAttributesData()
                .Select(attribute => _reflection.CustomAttributeData(attribute))
                .ToList();
    }
}