namespace Books.WebApi.Controllers
{
    using Books;
    using Home;
    using Hypermedia.AspNetCore.Siren;
    using Hypermedia.AspNetCore.Siren.Entities.Builder;

    public class MenuPartialResource : IHypermediaResource
    {
        public void Configure(IResourceBuilder builder)
        {
            builder
                .WithEmbeddedEntity(b => b
                    .WithClasses("menu")
                    .WithLink<BooksController>("books", c => c.Get(0, 12))
                    .WithLink<HomeController>("home", c => c.Get())
                );
        }
    }
}
