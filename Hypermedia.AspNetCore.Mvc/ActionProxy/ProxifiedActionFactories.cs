namespace Hypermedia.AspNetCore.Mvc.ActionProxy
{
    using AccessValidation;
    using ApiExploration;
    using HrefProviders;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Reflection;

    internal class ProxifiedActionFactories : IProxifiedActionFactories
    {
        private readonly IDictionary<MethodInfo, IProxifiedActionFactory> _factories = new ConcurrentDictionary<MethodInfo, IProxifiedActionFactory>();
        private readonly IApiActionDescriptors _descriptors;
        private readonly IHrefProviders _hrefProviders;
        private readonly IAccessValidators _accessValidators;
        private readonly IFieldsFactories _fieldFactories;

        public ProxifiedActionFactories(
            IApiActionDescriptors descriptors,
            IHrefProviders hrefProviders,
            IAccessValidators accessValidators,
            IFieldsFactories fieldFactories)
        {
            this._descriptors = descriptors;
            this._hrefProviders = hrefProviders;
            this._accessValidators = accessValidators;
            this._fieldFactories = fieldFactories;
        }

        public IProxifiedActionFactory Get(MethodInfo method)
        {
            if (this._factories.TryGetValue(method, out var factory))
            {
                return factory;
            }

            var descriptor = this._descriptors.Get(method);

            this._factories[method] = new ProxifiedActionFactory(
                this._hrefProviders.Get(descriptor),
                this._accessValidators.Get(descriptor),
                this._fieldFactories.Get(descriptor)
            );

            return this._factories[method];
        }
    }
}