namespace Hypermedia.AspNetCore.Siren
{
    using Entities;
    using Entities.Builder;

    public abstract class HypermediaResource<T> : IHypermediaResource where T : class
    {
        public abstract void Configure(ITypedEntityBuilder<T> builder);

        public void Configure(IEntityBuilder builder)
        {
            builder.With<T>(Configure);
        }
    }
}
