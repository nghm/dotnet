using System;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IMethodInfo : IReflectedTypeWrapper<MethodInfo>
    {
        MemberTypes MemberType { get; }

        ParameterInfo ReturnParameter { get; }

        IType ReturnType { get; }

        ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

        IType[] GetGenericArguments();

        IMethodInfo GetGenericMethodDefinition();

        IMethodInfo MakeGenericMethod(params IType[] typeArguments);

        IMethodInfo GetBaseDefinition();

        Delegate CreateDelegate(IType delegateType);

        Delegate CreateDelegate(IType delegateType, object target);

        bool Equals(object obj);

        int GetHashCode();
    }
}