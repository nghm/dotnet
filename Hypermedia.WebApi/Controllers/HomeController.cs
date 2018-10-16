using System.Linq;
using Hypermedia.AspNetCore.Siren;
using Hypermedia.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hypermedia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHypermedia hypermedia;

        public HomeController(IHypermedia hypermedia)
        {
            this.hypermedia = hypermedia;
        }

        [HttpGet]
        public virtual IActionResult Get()
        {
            var latestBooks = Enumerable
                .Range(0, 10)
                .Select(id => new BookListModel { Title = $"Book #no {id}", Id = id });

            return Ok(
                hypermedia
                    .Make()
                    .AddClasses("home")
                    .AddProperties(new {
                        title = "Home page, Hello World!",
                        description = "The entry point to the application"
                    })
                    .AddFrom<BooksController>(builder => builder
                        .AddLink(c => c.Get(), "menu", "books")
                        .AddLink(c => c.GetOne(1), "topRatedBook")
                        .AddEntities(latestBooks, (c, book) => c.GetOne(book.Id), "latest-book")
                    )
                    .Build()
            );
        }
    }
}
