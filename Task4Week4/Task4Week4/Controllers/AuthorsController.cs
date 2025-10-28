using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4Week4.Models;
using Task4Week4.Data;


namespace Task4Week4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly LibraryContext _context;
        public AuthorsController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<ActionResult<Author>> CreateAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Authors.Any(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Author>>> SearchAuthorsByName([FromQuery] string query)
        {
            var authors = await _context.Authors
                .Where(a => a.Name != null && a.Name.Contains(query))
                .ToListAsync();

            return Ok(authors);
        }

        [HttpGet("with-book-counts")]
        public async Task<ActionResult> GetAuthorsWithBookCounts()
        {
            var authors = await _context.Authors
                .Select(a => new
                {
                    AuthorId = a.Id,
                    AuthorName = a.Name,
                    BookCount = a.Books.Count 
                })
                .ToListAsync();

            return Ok(authors);
        }
    }
}