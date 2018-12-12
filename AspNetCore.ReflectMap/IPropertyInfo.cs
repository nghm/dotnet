using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IPropertyInfo : IReflectedTypeWrapper<PropertyInfo>
    {
        MemberTypes MemberType { get; }

        IType PropertyType { get; }

        PropertyAttributes Attributes { get; }

        bool IsSpecialName { get; }

        bool CanRead { get; }

        bool CanWrite { get; }

        IMethodInfo GetMethod { get; }

        IMethodInfo SetMethod { get; }

        ParameterInfo[] GetIndexParameters();

        IMethodInfo[] GetAccessors();

        IMethodInfo[] GetAccessors(bool nonPublic);

        IMethodInfo GetGetMethod();

        IMethodInfo GetGetMethod(bool nonPublic);

        IMethodInfo GetSetMethod();

        IMethodInfo GetSetMethod(bool nonPublic);

        IType[] GetOptionalCustomModifiers();

        IType[] GetRequiredCustomModifiers();

        object GetValue(object obj);

        object GetValue(object obj, object[] index);

        object GetValue(
            object obj,
            BindingFlags invokeAttr,
            Binder binder,
            object[] index,
            CultureInfo culture);

        object GetConstantValue();

        object GetRawConstantValue();

        void SetValue(object obj, object value);

        void SetValue(object obj, object value, object[] index);

        void SetValue(
            object obj,
            object value,
            BindingFlags invokeAttr,
            Binder binder,
            object[] index,
            CultureInfo culture);

        bool Equals(object obj);

        int GetHashCode();
    }
}