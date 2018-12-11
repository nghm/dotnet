using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    internal class ConstructorInfoWrapper : IConstructorInfo
    {
        private readonly ConstructorInfo _constructorInfo;

        public ConstructorInfoWrapper(ConstructorInfo constructorInfo)
        {
            _constructorInfo = constructorInfo;
        }

        public MemberTypes MemberType => _constructorInfo.MemberType;

        public object Invoke(object[] parameters)
            => _constructorInfo.Invoke(parameters);

        public object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
            => _constructorInfo.Invoke(invokeAttr, binder, parameters, culture);
    }
}