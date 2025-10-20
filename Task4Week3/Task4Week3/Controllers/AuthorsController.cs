using Microsoft.AspNetCore.Mvc;
using Task4Week3.Models;
using Task4Week3.Data;

namespace Task4Week3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAuthors()
        {
            return Ok(InMemoryData.Authors);
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthor(int id)
        {
            var author = InMemoryData.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> CreateAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); 
            }

            author.Id = InMemoryData.Authors.Any() ? InMemoryData.Authors.Max(a => a.Id) + 1 : 1;
            InMemoryData.Authors.Add(author);

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author updatedAuthor)
        {
            if (id != updatedAuthor.Id)
            {
                return BadRequest("ID в URL и в теле запроса не совпадают.");
            }

            var author = InMemoryData.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            author.Name = updatedAuthor.Name;
            author.DateOfBirth = updatedAuthor.DateOfBirth;

            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            var author = InMemoryData.Authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            InMemoryData.Authors.Remove(author);
            return NoContent();
        }
    }
}