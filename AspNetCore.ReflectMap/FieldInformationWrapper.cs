using System;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class FieldInformationWrapper : ReflectedTypeWrapper<FieldInfo>, IFieldInformation
    {
        public FieldInformationWrapper(IReflection reflection, FieldInfo fieldInfo)
            : base(reflection, fieldInfo)
        { }

        public MemberTypes MemberType
            => _underlyingType.MemberType;

        public FieldAttributes Attributes
            => _underlyingType.Attributes;

        public IType FieldType
            => _reflection.TypeOf(_underlyingType.FieldType);

        public bool IsInitOnly
            => _underlyingType.IsInitOnly;

        public bool IsLiteral
            => _underlyingType.IsLiteral;

        public bool IsNotSerialized
            => _underlyingType.IsNotSerialized;

        public bool IsPinvokeImpl
            => _underlyingType.IsPinvokeImpl;

        public bool IsSpecialName
            => _underlyingType.IsSpecialName;

        public bool IsStatic
            => _underlyingType.IsStatic;

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

        public bool IsSecurityCritical
            => _underlyingType.IsSecurityCritical;

        public bool IsSecuritySafeCritical
            => _underlyingType.IsSecuritySafeCritical;

        public bool IsSecurityTransparent
            => _underlyingType.IsSecurityTransparent;

        public RuntimeFieldHandle FieldHandle
            => _underlyingType.FieldHandle;

        public object GetValue(object obj)
            => _underlyingType.GetValue(obj);

        public void SetValue(object obj, object value)
            => _underlyingType.SetValue(obj, value);

        public void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
            => _underlyingType.SetValue(obj, value, invokeAttr, binder, culture);

        public void SetValueDirect(TypedReference obj, object value)
            => _underlyingType.SetValueDirect(obj, value);

        public object GetValueDirect(TypedReference obj)
            => _underlyingType.GetValueDirect(obj);

        public object GetRawConstantValue()
            => _underlyingType.GetRawConstantValue();

        public IType[] GetOptionalCustomModifiers()
            => _underlyingType.GetOptionalCustomModifiers().Select(modifier => this._reflection.TypeOf(modifier)).ToArray();

        public IType[] GetRequiredCustomModifiers()
            => _underlyingType.GetRequiredCustomModifiers().Select(modifier => this._reflection.TypeOf(modifier)).ToArray();
    }
}