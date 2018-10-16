using Hypermedia.AspNetCore.Siren.Entities;
using Hypermedia.AspNetCore.Siren.ProxyCollectors;
using System.Security.Claims;

namespace Hypermedia.AspNetCore.Siren
{
    class Hypermedia : IHypermedia
    {
        private readonly CachedProxyCollector proxyCollector;

        public Hypermedia(CachedProxyCollector proxyCollector)
        {
            this.proxyCollector = proxyCollector;
        }

        public IEntityBuilder Make()
        {
            return new EntityBuilder(proxyCollector, null);
        }
        public IEntityBuilder Make(ClaimsPrincipal claimsPrincipal)
        {
            return new EntityBuilder(proxyCollector, claimsPrincipal);
        }
    }
}
