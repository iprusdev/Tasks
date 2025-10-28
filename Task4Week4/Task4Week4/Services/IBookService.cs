using Task4Week4.Models;

namespace Task4Week4.Services
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooksAsync();
        Task<Book?> GetBookByIdAsync(int id);
        Task<Book> CreateBookAsync(Book book);
        Task<bool> UpdateBookAsync(int id, Book book);
        Task<bool> DeleteBookAsync(int id);
        Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
    }
}
