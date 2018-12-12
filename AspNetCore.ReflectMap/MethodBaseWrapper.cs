using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class MethodBaseWrapper : ReflectedTypeWrapper<MethodBase>, IMethodBase
    {
        public MethodBaseWrapper(IReflection reflection, MethodBase methodBase)
            : base(reflection, methodBase)
        { }

        public ParameterInfo[] GetParameters()
            => _underlyingType.GetParameters();

        public MethodAttributes Attributes
            => _underlyingType.Attributes;

        public MethodImplAttributes MethodImplementationFlags
            => _underlyingType.MethodImplementationFlags;

        public CallingConventions CallingConvention
            => _underlyingType.CallingConvention;

        public bool IsAbstract
            => _underlyingType.IsAbstract;

        public bool IsConstructor
            => _underlyingType.IsConstructor;

        public bool IsFinal
            => _underlyingType.IsFinal;

        public bool IsHideBySig
            => _underlyingType.IsHideBySig;

        public bool IsSpecialName
            => _underlyingType.IsSpecialName;

        public bool IsStatic
            => _underlyingType.IsStatic;

        public bool IsVirtual
            => _underlyingType.IsVirtual;

        public bool IsAssembly
            => _underlyingType.IsAssembly;

        public bool IsFamily
            => _underlyingType.IsFamily;

        public bool IsFamilyAndAssembly
            => _underlyingType.IsFamilyAndAssembly;

        public bool IsFamilyOrAssembly
            => _underlyingType.IsFamilyOrAssembly;

        public bool IsPrivate
            => _underlyingType.IsPrivate;

        public bool IsPublic
            => _underlyingType.IsPublic;

        public bool IsConstructedGenericMethod
            => _underlyingType.IsConstructedGenericMethod;

        public bool IsGenericMethod
            => _underlyingType.IsGenericMethod;

        public bool IsGenericMethodDefinition
            => _underlyingType.IsGenericMethodDefinition;

        public bool ContainsGenericParameters
            => _underlyingType.ContainsGenericParameters;

        public RuntimeMethodHandle MethodHandle
            => _underlyingType.MethodHandle;

        public bool IsSecurityCritical
            => _underlyingType.IsSecurityCritical;

        public bool IsSecuritySafeCritical
            => _underlyingType.IsSecuritySafeCritical;

        public bool IsSecurityTransparent
            => _underlyingType.IsSecurityTransparent;

        public MethodImplAttributes GetMethodImplementationFlags()
            => _underlyingType.GetMethodImplementationFlags();

        public MethodBody GetMethodBody()
            => _underlyingType.GetMethodBody();

        public IType[] GetGenericArguments()
            => _underlyingType.GetGenericArguments().Select(argument => _reflection.TypeOf(argument)).ToArray();

        public object Invoke(object obj, object[] parameters)
            => _underlyingType.Invoke(obj, parameters);

        public object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
            => _underlyingType.Invoke(obj, invokeAttr, binder, parameters, culture);
    }
}