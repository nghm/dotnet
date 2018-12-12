namespace AspNetCore.ReflectMap
{
    public interface IReflectedTypeWrapper<T>
    {
        T GetUnderlyingType();
    }
}