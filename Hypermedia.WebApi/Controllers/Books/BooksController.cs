﻿namespace Books.WebApi.Controllers.Books
{
    using AutoMapper;
    using Hypermedia.AspNetCore.Siren;
    using Infrastructure.Services;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICrudService<Book, Guid> _books;

        public BooksController(IMapper mapper, ICrudService<Book, Guid> books)
        {
            this._mapper = mapper;
            this._books = books;
        }

        [HttpGet]
        [HasMenu]
        public IActionResult Get(int pageNo = 0, int perPage = 12)
        {
            var books = this._books.Paginate(perPage, pageNo);
            var totalCount = this._books.Count();

            return Ok(new BooksResource(pageNo, perPage, totalCount, books));
        }

        [HttpGet("{id}")]
        [HasMenu]
        // [Authorize("CanEditBooks")]
        public IActionResult GetOne(Guid id, int pageNo = 0, int perPage = 12)
        {
            var book = this._books.One(id);

            if (book == null)
            {
                return NotFound();
            }

            return Ok(new BookResource(perPage, pageNo, book));
        }

        [HttpPatch("{id}")]
        public IActionResult Update(Guid id, [FromBody] EditBookModel editBookModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState.AsResource());
            }

            var mappedBook = this._mapper.Map<Book>(editBookModel);
            this._books.Update(id, mappedBook);

            return NoContent();
        }

        [HttpPost]
        public IActionResult Create([FromBody] NewBookModel newBookModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState.AsResource());
            }

            var mappedBook = this._mapper.Map<Book>(newBookModel);
            this._books.Create(mappedBook);

            return NoContent();
        }
    }
}