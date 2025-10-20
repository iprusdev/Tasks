using Microsoft.AspNetCore.Mvc;
using Task4Week3.Models;
using Task4Week3.Data;

namespace Task4Week3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks()
        {
            return Ok(InMemoryData.Books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBook(int id)
        {
            var book = InMemoryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> CreateBook(Book book)
        {
            if (!InMemoryData.Authors.Any(a => a.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Автор с таким ID не существует.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            book.Id = InMemoryData.Books.Any() ? InMemoryData.Books.Max(b => b.Id) + 1 : 1;
            InMemoryData.Books.Add(book);

            return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, Book updatedBook)
        {
            if (id != updatedBook.Id)
            {
                return BadRequest();
            }

            var book = InMemoryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            if (!InMemoryData.Authors.Any(a => a.Id == updatedBook.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Автор с таким ID не существует.");
                return BadRequest(ModelState);
            }

            book.Title = updatedBook.Title;
            book.PublishedYear = updatedBook.PublishedYear;
            book.AuthorId = updatedBook.AuthorId;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = InMemoryData.Books.FirstOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            InMemoryData.Books.Remove(book);
            return NoContent();
        }
    }
}