using System.Globalization;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface IConstructorInfo
    {
        MemberTypes MemberType { get; }

        object Invoke(object[] parameters);

        object Invoke(
            BindingFlags invokeAttr,
            Binder binder,
            object[] parameters,
            CultureInfo culture);

        bool Equals(object obj);

        int GetHashCode();
    }
}