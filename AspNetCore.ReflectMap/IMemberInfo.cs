using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IMemberInfo
    {
        MemberTypes MemberType { get; }
        string Name { get; }
        IType DeclaringType { get; }
        IType ReflectedType { get; }
        Module Module { get; }
        IEnumerable<ICustomAttributeData> CustomAttributes { get; }
        int MetadataToken { get; }
        bool HasSameMetadataDefinitionAs(IMemberInfo other);
        bool IsDefined(IType attributeType, bool inherit);
        object[] GetCustomAttributes(bool inherit);
        object[] GetCustomAttributes(IType attributeType, bool inherit);
        IList<ICustomAttributeData> GetCustomAttributesData();
        bool Equals(object obj);
        int GetHashCode();
    }
}