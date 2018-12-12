using System.Globalization;
using System.Linq;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class PropertyInfoWrapper : ReflectedTypeWrapper<PropertyInfo>, IPropertyInfo
    {
        public PropertyInfoWrapper(IReflection reflection, PropertyInfo propertyInfo)
            : base(reflection, propertyInfo)
        { }

        public MemberTypes MemberType
            => _underlyingType.MemberType;

        public IType PropertyType
            => _reflection.TypeOf(_underlyingType.PropertyType);

        public PropertyAttributes Attributes
            => _underlyingType.Attributes;

        public bool IsSpecialName
            => _underlyingType.IsSpecialName;

        public bool CanRead
            => _underlyingType.CanRead;

        public bool CanWrite
            => _underlyingType.CanWrite;

        public IMethodInfo GetMethod
            => _reflection.MethodInfo(_underlyingType.GetMethod);

        public IMethodInfo SetMethod
            => _reflection.MethodInfo(_underlyingType.SetMethod);

        public ParameterInfo[] GetIndexParameters()
            => _underlyingType.GetIndexParameters();

        public IMethodInfo[] GetAccessors()
            => _underlyingType.GetAccessors()
                .Select(accessor => _reflection.MethodInfo(accessor))
                .ToArray();

        public IMethodInfo[] GetAccessors(bool nonPublic)
            => _underlyingType.GetAccessors(nonPublic)
                .Select(accessor => _reflection.MethodInfo(accessor))
                .ToArray();

        public IMethodInfo GetGetMethod()
            => _reflection.MethodInfo(_underlyingType.GetGetMethod());

        public IMethodInfo GetGetMethod(bool nonPublic)
            => _reflection.MethodInfo(_underlyingType.GetGetMethod(nonPublic));

        public IMethodInfo GetSetMethod()
            => _reflection.MethodInfo(_underlyingType.GetSetMethod());

        public IMethodInfo GetSetMethod(bool nonPublic)
            => _reflection.MethodInfo(_underlyingType.GetSetMethod(nonPublic));

        public IType[] GetOptionalCustomModifiers()
            => _underlyingType.GetOptionalCustomModifiers()
                .Select(modifier => _reflection.TypeOf(modifier))
                .ToArray();

        public IType[] GetRequiredCustomModifiers()
            => _underlyingType.GetRequiredCustomModifiers()
                .Select(modifier => _reflection.TypeOf(modifier))
                .ToArray();

        public object GetValue(object obj)
            => _underlyingType.GetValue(obj);

        public object GetValue(object obj, object[] index)
            => _underlyingType.GetValue(obj, index);

        public object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
            => _underlyingType.GetValue(obj, invokeAttr, binder, index, culture);

        public object GetConstantValue()
            => _underlyingType.GetConstantValue();

        public object GetRawConstantValue()
            => _underlyingType.GetRawConstantValue();

        public void SetValue(object obj, object value)
            => _underlyingType.SetValue(obj, value);

        public void SetValue(object obj, object value, object[] index)
            => _underlyingType.SetValue(obj, value, index);

        public void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
            => _underlyingType.SetValue(obj, value, invokeAttr, binder, index, culture);
    }
}