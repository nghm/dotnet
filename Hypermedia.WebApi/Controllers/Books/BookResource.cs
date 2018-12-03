namespace Books.WebApi.Controllers.Books
{
    using Hypermedia.AspNetCore.Siren;
    using Hypermedia.AspNetCore.Siren.Entities.Builder;
    using Infrastructure.Services;
    using Models;
    using System.Collections.Generic;

    public class BookResource : IHypermediaResource
    {
        private readonly int _perPage;
        private readonly int _pageNo;
        private readonly Book _book;
        private readonly Dictionary<string, string> _allowedTags = new Dictionary<string, string>()
        {
            ["testName"] = "Test value"
        };

        private EditBookModel EditBookModel => new EditBookModel
        {
            Title = this._book.Title,
            Description = this._book.Description,
            IsFree = this._book.IsFree,
            Tags = this._book.Tags
        };

        public BookResource(int perPage, int pageNo, Book book)
        {
            this._perPage = perPage;
            this._pageNo = pageNo;
            this._book = book;
        }

        public void Configure(IResourceBuilder builder)
        {
            builder
                .WithClasses("book", "details")
                .WithProperties<BookDetailsModel>(this._book)
                .WithLinks<BooksController>(
                    ("books", c => c.Get(this._pageNo, this._perPage), new[] { "parent" }),
                    ("self", c => c.GetOne(this._book.Id, this._pageNo, this._perPage), new string[] { })
                )
                .WithActions<BooksController, EditBookModel>(
                    ("update", c => c.Update(this._book.Id, this.EditBookModel), action => action.WithOptions(b => b.Tags, this._allowedTags))
                );
        }
    }
}