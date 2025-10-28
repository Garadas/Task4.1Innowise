using Microsoft.AspNetCore.Mvc;
using quest5.Models;
using quest5.Services;

namespace quest5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _books;

        public BooksController(IBookService books)
        {
            _books = books;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll() => Ok(_books.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Book> Get(int id) => Ok(_books.GetById(id));

        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book book)
        {
            var created = _books.Create(book);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            _books.Update(id, book);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            _books.Delete(id);
            return NoContent();
        }

        // --- LINQ-запросы ---

        [HttpGet("after/{year}")]
        public IActionResult GetAfterYear(int year)
        {
            return Ok(_books.GetBooksAfterYear(year));
        }

        [HttpGet("authors-with-count")]
        public IActionResult GetAuthorsWithCount()
        {
            return Ok(_books.GetAuthorsWithBookCount());
        }

        [HttpGet("find-author/{name}")]
        public IActionResult FindAuthor(string name)
        {
            return Ok(_books.FindAuthorsByName(name));
        }
    }
}