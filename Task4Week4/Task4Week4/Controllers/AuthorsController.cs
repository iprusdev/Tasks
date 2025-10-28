using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4Week4.Data;
using Task4Week4.Models;
using Task4Week4.Services;


namespace Task4Week4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return Ok(await _authorService.GetAllAuthorsAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _authorService.GetAuthorByIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newAuthor = await _authorService.CreateAuthorAsync(author);
            return CreatedAtAction(nameof(GetAuthor), new { id = newAuthor.Id }, newAuthor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authorService.UpdateAuthorAsync(id, author);
            if (!result)
            {
                return BadRequest("ID не совпадает или автор не найден.");
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthorAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Author>>> SearchAuthors([FromQuery] string query)
        {
            return Ok(await _authorService.SearchAuthorsAsync(query));
        }

        [HttpGet("with-book-counts")]
        public async Task<ActionResult> GetAuthorsWithBookCounts()
        {
            return Ok(await _authorService.GetAuthorsWithBookCountsAsync());
        }
    }
}