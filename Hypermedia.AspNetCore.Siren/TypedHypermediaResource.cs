namespace Hypermedia.AspNetCore.Siren
{
    using Entities;

    public abstract class TypedHypermediaResource<T> : HypermediaResource where T : class
    {
        public abstract void Configure(ITypedEntityBuilder<T> builder);

        public void Configure(IEntityBuilder builder)
        {
            builder.With<T>(Configure);;
        }
    }
}
