using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task4Week4.Data;
using Task4Week4.Models;
using Task4Week4.Services;

namespace Task4Week4.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return Ok(await _bookService.GetAllBooksAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newBook = await _bookService.CreateBookAsync(book);
                return CreatedAtAction(nameof(GetBook), new { id = newBook.Id }, newBook);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _bookService.UpdateBookAsync(id, book);
                if (!result)
                {
                    return BadRequest("ID не совпадает.");
                }
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("published-after/{year}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooksPublishedAfter(int year)
        {
            return Ok(await _bookService.GetBooksPublishedAfterAsync(year));
        }
    }
}