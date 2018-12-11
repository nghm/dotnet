using System;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IMethodInfo
    {
        MemberTypes MemberType { get; }
        ParameterInfo ReturnParameter { get; }
        IType ReturnType { get; }
        ICustomAttributeProvider ReturnTypeCustomAttributes { get; }
        IType[] GetGenericArguments();
        MethodInfo GetGenericMethodDefinition();
        MethodInfo MakeGenericMethod(params IType[] typeArguments);
        MethodInfo GetBaseDefinition();
        Delegate CreateDelegate(IType delegateType);
        Delegate CreateDelegate(IType delegateType, object target);
        bool Equals(object obj);
        int GetHashCode();
    }
}