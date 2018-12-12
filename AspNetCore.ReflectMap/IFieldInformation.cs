using System;
using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IFieldInformation : IReflectedTypeWrapper<FieldInfo>
    {
        MemberTypes MemberType { get; }

        FieldAttributes Attributes { get; }

        IType FieldType { get; }

        bool IsInitOnly { get; }

        bool IsLiteral { get; }

        bool IsNotSerialized { get; }

        bool IsPinvokeImpl { get; }

        bool IsSpecialName { get; }

        bool IsStatic { get; }

        bool IsAssembly { get; }

        bool IsFamily { get; }

        bool IsFamilyAndAssembly { get; }

        bool IsFamilyOrAssembly { get; }

        bool IsPrivate { get; }

        bool IsPublic { get; }

        bool IsSecurityCritical { get; }

        bool IsSecuritySafeCritical { get; }

        bool IsSecurityTransparent { get; }

        RuntimeFieldHandle FieldHandle { get; }

        bool Equals(object obj);

        int GetHashCode();

        object GetValue(object obj);

        void SetValue(object obj, object value);

        void SetValue(
            object obj,
            object value,
            BindingFlags invokeAttr,
            Binder binder,
            CultureInfo culture);

        void SetValueDirect(TypedReference obj, object value);

        object GetValueDirect(TypedReference obj);

        object GetRawConstantValue();

        IType[] GetOptionalCustomModifiers();

        IType[] GetRequiredCustomModifiers();
    }
}