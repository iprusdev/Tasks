using Task4Week4.Models;
namespace Task4Week4.Repositories
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task UpdateAsync(Author author);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Author>> FindAsync(string query);
        Task<object> GetAllWithBookCountsAsync();
    }
}
