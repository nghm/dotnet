namespace Hypermedia.WebApi.Controllers.Home
{
    using System;
    using System.Collections.Generic;
    using AspNetCore.Siren;
    using AspNetCore.Siren.Entities;
    using Books;
    using Models;

    internal class HomeResource : HypermediaResource
    {
        private readonly IEnumerable<BookListModel> _latestBooks;
        private readonly Guid _topRatedBookId;

        public HomeResource(IEnumerable<BookListModel> latestBooks, Guid topRatedBookId)
        {
            this._latestBooks = latestBooks;
            this._topRatedBookId = topRatedBookId;
        }

        public void Configure(IEntityBuilder builder)
        {
            builder
                .WithClasses("home")
                .WithProperties(new
                {
                    title = "Home page, Hello World!",
                    description = "The entry point to the application"
                })
                .WithLink<BooksController>("books", c => c.Get(), "menu")
                .WithLink<BooksController>("topRatedBook", c => c.GetOne(this._topRatedBookId))
                .WithEntities(
                    this._latestBooks, 
                    (BooksController c, BookListModel book) => c.GetOne(book.Id), 
                    new [] { "latest-book" }
                );
        }
    }
}