using Task4Week4.Models;

namespace Task4Week4.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(int id);
        Task<IEnumerable<Book>> GetPublishedAfterYearAsync(int year);
    }
}
