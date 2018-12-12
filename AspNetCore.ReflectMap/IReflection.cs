using System;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IReflection
    {
        IType TypeOf(Type type);

        IConstructorInfo ConstructorInfo(ConstructorInfo constructorInfo);

        ICustomAttributeData CustomAttributeData(CustomAttributeData customAttributeData);

        IFieldInformation FieldInfo(FieldInfo fieldInfo);

        IMemberInfo MemberInfo(MemberInfo memberInfo);

        IMethodInfo MethodInfo(MethodInfo methodInfo);

        ITypeInformation TypeInfo(TypeInfo typeInfo);

        IPropertyInfo PropertyInfo(PropertyInfo propertyInfo);

        IMethodBase MethodBase(MethodBase methodBase);
    }
}
