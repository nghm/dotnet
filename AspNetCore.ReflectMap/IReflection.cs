using System;

namespace AspNetCore.ReflectMap
{
    public interface IReflection
    {
        IType TypeOf(Type type);
    }
}
