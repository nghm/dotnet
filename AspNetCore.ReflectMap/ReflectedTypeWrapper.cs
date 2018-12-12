namespace AspNetCore.ReflectMap
{
    internal abstract class ReflectedTypeWrapper<T> : IReflectedTypeWrapper<T>
    {
        protected readonly IReflection _reflection;
        protected readonly T _underlyingType;

        protected ReflectedTypeWrapper(IReflection reflection, T underlyingType)
        {
            _reflection = reflection;
            _underlyingType = underlyingType;
        }

        public T GetUnderlyingType() => _underlyingType;
    }
}
