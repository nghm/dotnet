namespace Hypermedia.AspNetCore.Mvc
{
    using ApiExploration;
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    internal class HrefFactories : IHrefFactories
    {
        private readonly IDictionary<IApiActionDescriptor, IHrefFactory> _factories = new ConcurrentDictionary<IApiActionDescriptor, IHrefFactory>();

        public IHrefFactory Get(IApiActionDescriptor descriptor)
        {
            if (!this._factories.TryGetValue(descriptor, out var factory))
            {
                factory = new HrefFactory(descriptor.Template, descriptor.Query, descriptor.Route);

                this._factories[descriptor] = factory;
            }

            return factory;
        }
    }
}
