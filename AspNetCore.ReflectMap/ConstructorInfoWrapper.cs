using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class ConstructorInfoWrapper : ReflectedTypeWrapper<ConstructorInfo>, IConstructorInfo
    {
        public ConstructorInfoWrapper(IReflection reflection, ConstructorInfo constructorInfo)
            : base(reflection, constructorInfo)
        { }

        public MemberTypes MemberType => _underlyingType.MemberType;

        public object Invoke(object[] parameters)
            => _underlyingType.Invoke(parameters);

        public object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
            => _underlyingType.Invoke(invokeAttr, binder, parameters, culture);
    }
}