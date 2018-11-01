namespace Hypermedia.AspNetCore.Siren.ProxyCollectors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Castle.DynamicProxy;
    using Moq.AutoMock;

    internal class CastleProxyCollector : IProxyCollector
    {
        private readonly IDictionary<Type, object> _cache = new Dictionary<Type, object>();

        private readonly ProxyGenerator _generator = new ProxyGenerator();
        private readonly AutoMocker _mocker = new AutoMocker();
        
        public CollectedMethodCall ProxyCollectOne<T>(Action<T> select) where T : class
        {
            var type = typeof(T);

            if (!this._cache.ContainsKey(type))
            {
                var interceptor = new CallCollectorInterceptor();
                var ctor = type.GetConstructors().First();
                var arguments = ctor
                    .GetParameters()
                    .Select(parameter => this._mocker.Get(parameter.ParameterType))
                    .ToArray();

                this._cache[type] = this._generator.CreateClassProxy(type, arguments, interceptor);
            }

            var proxy = this._cache[type];

            try
            {
                @select.Invoke(proxy as T);
            }
            catch (CallCollectionFinishedException collectionResult)
            {
                return collectionResult.Call;
            }

            return null;
        }
    }
}