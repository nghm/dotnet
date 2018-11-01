using Hypermedia.AspNetCore.Siren;
using Hypermedia.WebApi.Models;
using Hypermedia.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Hypermedia.WebApi.Controllers
{
    using System;
    using System.ComponentModel.DataAnnotations;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IHypermedia Hypermedia { get; }
        private BooksService Books { get; }

        public BooksController(IHypermedia hypermedia, BooksService books)
        {
            this.Hypermedia = hypermedia;
            this.Books = books;
        }

        [HttpGet]
        public virtual IActionResult Get(
            int pageNo = 0, 
            int perPage = 12
        )
        {
            var books = Books.Paginate(perPage, pageNo);
            var allCount = Books.Count();

            var hasPrevious = pageNo > 0;
            var hasNext = (float) allCount / perPage > pageNo + 1;

            return Ok(
                this.Hypermedia
                    .Make()
                    .AddClasses("books")
                    .AddProperties(new { name = "Books", pageNo, perPage })
                    .AddFrom<BooksController>(builder =>
                    {
                        builder
                            .AddEntity(c => c.GetOne(0), "book-latest")
                            .AddEntities(books, (bookBuilder, book) => bookBuilder
                                .AddClasses("recent-book", "book-embedded", book.Id % 2 == 0 ? "even" : "odd")
                                .AddProperties(book)
                                .AddLink<BooksController>(c => c.GetOne(book.Id, pageNo, perPage), "details"))
                            .AddLink(c => c.Get(pageNo, perPage), "self")
                            .AddLink(c => c.Get(0, perPage), "first")
                            .AddLink(c => c.Get(allCount / perPage, perPage), "last")
                            .AddAction(c => c.Create(new NewBookModel {Title = "(empty)", Description = "(empty)"}),
                                "create");

                        if (hasNext)
                        {
                            builder.AddLink(c => c.Get(pageNo + 1, perPage), "next");
                        }

                        if (hasPrevious)
                        {
                            builder.AddLink(c => c.Get(pageNo - 1, perPage), "previous");
                        }
                    })
                    .Build()
            );
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetOne(int id, int pageNo = 0, int perPage = 12)
        {
            var oneBook = Books.One(id);

            return Ok(
                Hypermedia.Make()
                    .AddClasses("book", "details")
                    .AddProperties(oneBook)
                    .AddFrom<BooksController>(builder => builder
                        .AddLink(c => c.Get(pageNo, perPage), "parent", "books")
                        .AddLink(c => c.GetOne(id, pageNo, perPage), "self")
                        .AddAction(c => c.Update(id, new EditBookModel
                        {
                            Title = oneBook.Title,
                            Description = oneBook.Description,
                            Status = oneBook.Status,
                            IsFree = oneBook.IsFree
                        }), "update")
                    )
                    .Build()
            );
        }

        [HttpPatch("{id}")]
        public virtual IActionResult Update(int id, [FromBody] EditBookModel editBookModel)
        {
            if (id >= 0 && ModelState.IsValid)
            {
                Books.Update(id, new Book
                {
                    Id = id,
                    Title = editBookModel.Title,
                    Description = editBookModel.Description,
                    Status = editBookModel.Status,
                    IsFree = editBookModel.IsFree
                });

                return NoContent();
            }

            return BadRequest(
                new
                {
                    @class = new[] { "error" },
                    properties = ModelState
                        .ToDictionary(
                            modelError => modelError
                                .Key,
                            modelError => modelError
                                .Value
                                .Errors
                                .Select(error => error.ErrorMessage)
                        )
                }
            );
        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] NewBookModel newBookModel)
        {
            if (ModelState.IsValid)
            {
                Books.Create(new Book
                {
                    Title = newBookModel.Title,
                    Description = newBookModel.Description
                });

                return NoContent();
            }

            return BadRequest(
                new
                {
                    @class = new[] { "error" },
                    properties = ModelState
                        .ToDictionary(
                            modelError => modelError
                                .Key,
                            modelError => modelError
                                .Value
                                .Errors
                                .Select(error => error.ErrorMessage)
                        )
                }
            );
        }
    }
}