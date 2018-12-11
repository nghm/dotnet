using System.Collections.Generic;
using System.Reflection;

namespace AspNetCore.ReflectMap
{
    public interface ICustomAttributeData
    {
        string ToString();

        int GetHashCode();

        bool Equals(object obj);

        IType AttributeType { get; }

        IConstructorInfo Constructor { get; }

        IList<CustomAttributeTypedArgument> ConstructorArguments { get; }

        IList<CustomAttributeNamedArgument> NamedArguments { get; }
    }
}