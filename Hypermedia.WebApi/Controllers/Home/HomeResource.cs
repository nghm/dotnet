namespace Books.WebApi.Controllers.Home
{
    using Hypermedia.AspNetCore.Siren;
    using Hypermedia.AspNetCore.Siren.Entities;

    internal class HomeResource : IHypermediaResource
    {
        public void Configure(IEntityBuilder builder)
        {
            builder
                .WithClasses("home");
        }
    }
}