namespace Books.WebApi.Controllers.Books
{
    using Hypermedia.AspNetCore.Siren;
    using Hypermedia.AspNetCore.Siren.Entities.Builder;
    using Infrastructure.Services;
    using Models;

    internal class BooksResource : IHypermediaResource
    {
        private readonly int _totalCount;
        private readonly Book[] _books;
        private readonly int _perPage;
        private readonly int _pageNo;

        private NewBookModel NewBookModel => new NewBookModel
        {
            Title = "(empty)",
            Description = "(empty)"
        };

        public BooksResource(int pageNo, int perPage, int totalCount, Book[] books)
        {
            this._pageNo = pageNo;
            this._perPage = perPage;

            this._totalCount = totalCount;
            this._books = books;
        }

        public void Configure(IApiAwareEntityBuilder builder)
        {
            builder
                .WithClasses("books")
                .WithProperties(new { name = "Books" })
                .WithEmbeddedEntitiesForEach(this._books, (b, book) => b
                    .WithClasses("preview-book")
                    .WithProperties<BookPreviewModel>(book)
                    .WithLink<BooksController>(
                        "details",
                        c => c.GetOne(book.Id, this._pageNo, this._perPage)
                    )
                )
                .WithAction<BooksController>("create", c => c.Create(this.NewBookModel))
                .WithLinks<BooksController>(
                    ("self", c => c.Get(this._pageNo, this._perPage), null),
                    ("first", c => c.Get(0, this._perPage), null),
                    ("last", c => c.Get(this._totalCount / this._perPage, this._perPage), null),
                    ("next", c => c.Get(this._pageNo + 1, this._perPage), null),
                    ("previous", c => c.Get(this._pageNo - 1, this._perPage), null)
                );
        }

    }
}