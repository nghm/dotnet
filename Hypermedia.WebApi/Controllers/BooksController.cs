namespace Hypermedia.WebApi.Controllers
{
    using Models;
    using Services;
    using Microsoft.AspNetCore.Mvc;
    using AspNetCore.Siren;
    using AutoMapper;

    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly BooksService _books;

        public BooksController(IMapper mapper, BooksService books)
        {
            this._mapper = mapper;
            this._books = books;
        }

        [HttpGet]
        public virtual IActionResult Get(
            int pageNo = 0, 
            int perPage = 12
        )
        {
            var books = this._books.Paginate(perPage, pageNo);
            var totalCount = this._books.Count();

            return Ok(
                new BooksResource(
                    pageNo, perPage, 
                    totalCount, books));
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetOne(int id, int pageNo = 0, int perPage = 12)
        {
            var book = this._books.One(id);

            return Ok(
                new BookResource(
                    perPage, pageNo, 
                    book));
        }

        [HttpPatch("{id}")]
        public virtual IActionResult Update(int id, [FromBody] EditBookModel editBookModel)
        {
            if (id < 0 || !this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState.ToResource());
            }

            var mappedBook = this._mapper.Map<Book>(editBookModel);
            this._books.Update(id, mappedBook);

            return NoContent();

        }

        [HttpPost]
        public virtual IActionResult Create([FromBody] NewBookModel newBookModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState.ToResource());
            }

            var mappedBook = this._mapper.Map<Book>(newBookModel);
            this._books.Create(mappedBook);

            return NoContent();
        }
    }
}