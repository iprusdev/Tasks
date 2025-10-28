using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4Week4.Data;
using Task4Week4.Models;

namespace Task4Week4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryContext _context;

        public BooksController(LibraryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.Include(b => b.Author).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == book.AuthorId);
            if (!authorExists)
            {
                ModelState.AddModelError("AuthorId", "Автор с таким ID не существует.");
                return BadRequest(ModelState);
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }
            _context.Entry(book).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("published-after/{year}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksPublishedAfter(int year)
        {
            var books = await _context.Books
                .Where(b => b.PublishedYear > year)
                .Include(b => b.Author)
                .ToListAsync();

            return Ok(books);
        }
    }
}