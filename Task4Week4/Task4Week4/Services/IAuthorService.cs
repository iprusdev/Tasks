using Task4Week4.Models;

namespace Task4Week4.Services
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> GetAllAuthorsAsync();
        Task<Author?> GetAuthorByIdAsync(int id);
        Task<Author> CreateAuthorAsync(Author author);
        Task<bool> UpdateAuthorAsync(int id, Author author);
        Task<bool> DeleteAuthorAsync(int id);
        Task<IEnumerable<Author>> SearchAuthorsAsync(string query);
        Task<object> GetAuthorsWithBookCountsAsync();
    }
}
