using Hypermedia.AspNetCore.Siren.Entities;
using Hypermedia.AspNetCore.Siren.ProxyCollectors;

namespace Hypermedia.AspNetCore.Siren
{
    class Hypermedia : IHypermedia
    {
        private readonly IProxyCollector proxyCollector;

        public Hypermedia(IProxyCollector proxyCollector)
        {
            this.proxyCollector = proxyCollector;
        }

        public IEntityBuilder Make()
        {
            return new EntityBuilder(proxyCollector, null);
        }
    }
}
