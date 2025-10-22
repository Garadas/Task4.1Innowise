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
        public ActionResult<Book> Get(int id)
        {
            var book = _books.GetById(id);
            if (book == null) return NotFound();
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book book)
        {
            try
            {
                var created = _books.Create(book);
                return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Book book)
        {
            try
            {
                if (!_books.Update(id, book)) return NotFound();
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!_books.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
