using System.Linq;
using Hypermedia.AspNetCore.Siren;
using Hypermedia.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hypermedia.WebApi.Controllers
{
    using System.Collections.Generic;
    using AspNetCore.Siren.Entities;

    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public virtual IActionResult Get()
        {
            var latestBooks = Enumerable
                .Range(0, 10)
                .Select(id => new BookListModel { Title = $"Book #no {id}", Id = id });

            return Ok(new HomeResource(latestBooks));
        }
    }

    internal class HomeResource : HypermediaResource
    {
        private readonly IEnumerable<BookListModel> _latestBooks;

        public HomeResource(IEnumerable<BookListModel> latestBooks)
        {
            this._latestBooks = latestBooks;
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
                .WithLink<BooksController>("topRatedBook", c => c.GetOne(1))
                .WithEntities(
                    this._latestBooks, 
                    (BooksController c, BookListModel book) => c.GetOne(book.Id), 
                    new [] { "latest-book" }
                );
        }
    }
}
