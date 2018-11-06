namespace Books.WebApi.Controllers.Books
{
    using Hypermedia.AspNetCore.Siren;
    using Hypermedia.AspNetCore.Siren.Entities;
    using Infrastructure.Services;
    using Models;

    public class BookResource : TypedHypermediaResource<BooksController>
    {
        private readonly int _perPage;
        private readonly int _pageNo;
        private readonly Book _book;

        private EditBookModel EditBookModel => new EditBookModel
        {
            Title = this._book.Title,
            Description = this._book.Description,
            Status = this._book.Status,
            IsFree = this._book.IsFree
        };

        public BookResource(int perPage, int pageNo, Book book)
        {
            this._perPage = perPage;
            this._pageNo = pageNo;
            this._book = book;
        }

        public override void Configure(ITypedEntityBuilder<BooksController> builder)
        {
            builder
                .WithClasses("book", "details")
                .WithProperties(this._book)
                .WithLink("books", c => c.Get(this._pageNo, this._perPage), "parent")
                .WithLink("self", c => c.GetOne(this._book.Id, this._pageNo, this._perPage))
                .WithAction("update", c => c.Update(this._book.Id, this.EditBookModel));
        }
    }
}