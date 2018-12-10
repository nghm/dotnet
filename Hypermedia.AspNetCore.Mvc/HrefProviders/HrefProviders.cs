namespace Hypermedia.AspNetCore.Mvc.HrefProviders
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using ApiExploration;

    internal class HrefProviders : IHrefProviders
    {
        private readonly IDictionary<IApiActionDescriptor, IHrefProvider> _factories = new ConcurrentDictionary<IApiActionDescriptor, IHrefProvider>();

        public IHrefProvider Get(IApiActionDescriptor descriptor)
        {
            if (!this._factories.TryGetValue(descriptor, out var factory))
            {
                factory = new HrefProvider(descriptor.Template, descriptor.Query, descriptor.Route);

                this._factories[descriptor] = factory;
            }

            return factory;
        }
    }
}
