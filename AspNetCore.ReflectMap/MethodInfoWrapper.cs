using System;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class MethodInfoWrapper : ReflectedTypeWrapper<MethodInfo>, IMethodInfo
    {
        public MethodInfoWrapper(IReflection reflection, MethodInfo methodInfo) : base(reflection, methodInfo)
        { }

        public MemberTypes MemberType
            => _underlyingType.MemberType;

        public ParameterInfo ReturnParameter
            => _underlyingType.ReturnParameter;

        public IType ReturnType
            => _reflection.TypeOf(_underlyingType.ReturnType);

        public ICustomAttributeProvider ReturnTypeCustomAttributes
            => _underlyingType.ReturnTypeCustomAttributes;

        public IType[] GetGenericArguments()
            => _underlyingType.GetGenericArguments().Select(argument => _reflection.TypeOf(argument)).ToArray();

        public IMethodInfo GetGenericMethodDefinition()
            => _reflection.MethodInfo(_underlyingType.GetGenericMethodDefinition());

        public IMethodInfo MakeGenericMethod(params IType[] typeArguments)
            => _reflection.MethodInfo(
                _underlyingType.MakeGenericMethod(
                    typeArguments.Select(argument => argument.GetUnderlyingType()).ToArray()));

        public IMethodInfo GetBaseDefinition()
            => _reflection.MethodInfo(_underlyingType.GetBaseDefinition());

        public Delegate CreateDelegate(IType delegateType)
            => _underlyingType.CreateDelegate(delegateType.GetUnderlyingType());

        public Delegate CreateDelegate(IType delegateType, object target)
            => _underlyingType.CreateDelegate(delegateType.GetUnderlyingType(), target);
    }
}