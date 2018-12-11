using System;
using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IMethodBase
    {
        ParameterInfo[] GetParameters();
        MethodAttributes Attributes { get; }
        MethodImplAttributes MethodImplementationFlags { get; }
        CallingConventions CallingConvention { get; }
        bool IsAbstract { get; }
        bool IsConstructor { get; }
        bool IsFinal { get; }
        bool IsHideBySig { get; }
        bool IsSpecialName { get; }
        bool IsStatic { get; }
        bool IsVirtual { get; }
        bool IsAssembly { get; }
        bool IsFamily { get; }
        bool IsFamilyAndAssembly { get; }
        bool IsFamilyOrAssembly { get; }
        bool IsPrivate { get; }
        bool IsPublic { get; }
        bool IsConstructedGenericMethod { get; }
        bool IsGenericMethod { get; }
        bool IsGenericMethodDefinition { get; }
        bool ContainsGenericParameters { get; }
        RuntimeMethodHandle MethodHandle { get; }
        bool IsSecurityCritical { get; }
        bool IsSecuritySafeCritical { get; }
        bool IsSecurityTransparent { get; }
        MethodImplAttributes GetMethodImplementationFlags();
        MethodBody GetMethodBody();
        IType[] GetGenericArguments();
        object Invoke(object obj, object[] parameters);

        object Invoke(
            object obj,
            BindingFlags invokeAttr,
            Binder binder,
            object[] parameters,
            CultureInfo culture);

        bool Equals(object obj);
        int GetHashCode();
    }
}