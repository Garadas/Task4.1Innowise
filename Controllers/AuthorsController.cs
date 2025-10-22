using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quest5.Models;
using quest5.Services;

namespace quest5.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authors;

        public AuthorsController(IAuthorService authors)
        {
            _authors = authors;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAll() => Ok(_authors.GetAll());

        [HttpGet("{id:int}")]
        public ActionResult<Author> Get(int id)
        {
            var author = _authors.GetById(id);
            if (author == null) return NotFound();
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> Create([FromBody] Author author)
        {
            var created = _authors.Create(author);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Author author)
        {
            if (!_authors.Update(id, author)) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            if (!_authors.Delete(id)) return NotFound();
            return NoContent();
        }
    }
}
