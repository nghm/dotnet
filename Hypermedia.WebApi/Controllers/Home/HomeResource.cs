namespace Books.WebApi.Controllers.Home
{
    using Books;
    using Feedback;
    using Hypermedia.AspNetCore.Siren;
    using Hypermedia.AspNetCore.Siren.Entities.Builder;

    internal class HomeResource : IHypermediaResource
    {
        public void Configure(IApiAwareEntityBuilder builder)
        {
            builder
                .WithClasses("home")
                .WithEmbeddedEntity(b => b
                    .WithClasses("feedback")
                    .WithAction<FeedbackController>("post", c => c.Post(new FeedbackModel()))
                )
                .WithEmbeddedEntity(b => b
                    .WithClasses("book-counts", "analytic")
                    .WithProperties(new { newBooks = 11, totalBooks = 110, rentedBooks = 24 })
                    .WithLink<BooksController>("books", c => c.Get(0, 12))
                )
                .WithEmbeddedEntity(b => b
                    .WithClasses("author-counts", "analytic")
                    .WithProperties(new { newAuthors = 11, totalAuthors = 110 })
                    .WithEmbeddedEntity(ab => ab
                        .WithClasses("best-selling-author", "author")
                        .WithProperties(new { name = "J.P.", book = "Maps of meaning" })
                    )
                );
        }
    }
}