namespace Hypermedia.WebApi.Controllers.Books
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using AspNetCore.Siren;
    using AspNetCore.Siren.Entities;
    using Models;
    using Services;

    internal class BooksResource : TypedHypermediaResource<BooksController>
    {
        private readonly int _totalCount;
        private readonly bool _hasPrevious;
        private readonly bool _hasNext;
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
            this._hasPrevious = pageNo > 0;
            this._hasNext = (float)totalCount / perPage > pageNo + 1;
            this._books = books;
        }

        public override void Configure(ITypedEntityBuilder<BooksController> builder)
        {
            builder
                .WithClasses("books")
                .WithProperties(new { name = "Books" })
                .WithEntities(this._books, (b, book) => b
                    .WithClasses("recent-book", "book-embedded")
                    .WithProperties(book)
                    .WithLink<BooksController>("details", c => c.GetOne(book.Id, this._pageNo, this._perPage)))
                .WithAction("create", c => c.Create(this.NewBookModel))
                .WithLinks(new Dictionary<string, Expression<Action<BooksController>>>
                {
                    ["self"] = c => c.Get(this._pageNo, this._perPage),
                    ["first"] = c => c.Get(0, this._perPage),
                    ["last"] = c => c.Get(this._totalCount / this._perPage, this._perPage)
                })
                .WithLinks(GetOptionalLinks());
        }

        private Dictionary<string, Expression<Action<BooksController>>> GetOptionalLinks()
        {
            var links = new Dictionary<string, Expression<Action<BooksController>>>();

            if (this._hasNext)
            {
                links.Add("next", c => c.Get(this._pageNo + 1, this._perPage));
            }

            if (this._hasPrevious)
            {
                links.Add("previous", c => c.Get(this._pageNo - 1, this._perPage));
            }

            return links;
        }
    }
}