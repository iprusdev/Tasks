using Task4Week4.Models;
using Task4Week4.Repositories;

namespace Task4Week4.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public Task<IEnumerable<Book>> GetAllBooksAsync()
        {
            return _bookRepository.GetAllAsync();
        }

        public Task<Book?> GetBookByIdAsync(int id)
        {
            return _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> CreateBookAsync(Book book)
        {
            if (!await _authorRepository.ExistsAsync(book.AuthorId))
            {
                throw new ArgumentException("Указанный автор не существует.");
            }
            return await _bookRepository.AddAsync(book);
        }

        public async Task<bool> UpdateBookAsync(int id, Book book)
        {
            if (id != book.Id)
            {
                return false;
            }

            if (!await _authorRepository.ExistsAsync(book.AuthorId))
            {
                throw new ArgumentException("Указанный автор не существует.");
            }

            await _bookRepository.UpdateAsync(book);
            return true;
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return false;
            }
            await _bookRepository.DeleteAsync(id);
            return true;
        }

        public Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year)
        {
            return _bookRepository.GetPublishedAfterYearAsync(year);
        }
    }
}
