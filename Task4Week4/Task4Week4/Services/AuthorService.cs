using Task4Week4.Models;
using Task4Week4.Repositories;

namespace Task4Week4.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public Task<IEnumerable<Author>> GetAllAuthorsAsync()
        {
            return _authorRepository.GetAllAsync();
        }

        public Task<Author?> GetAuthorByIdAsync(int id)
        {
            return _authorRepository.GetByIdAsync(id);
        }

        public Task<Author> CreateAuthorAsync(Author author)
        {
            return _authorRepository.AddAsync(author);
        }

        public async Task<bool> UpdateAuthorAsync(int id, Author author)
        {
            if (id != author.Id)
            {
                return false; 
            }
            if (!await _authorRepository.ExistsAsync(id))
            {
                return false; 
            }
            await _authorRepository.UpdateAsync(author);
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                return false;
            }

            if (author.Books.Any())
            {
                throw new InvalidOperationException("Нельзя удалить автора, у которого есть книги.");
            }

            await _authorRepository.DeleteAsync(id);
            return true;
        }

        public Task<IEnumerable<Author>> SearchAuthorsAsync(string query)
        {
            return _authorRepository.FindAsync(query);
        }

        public Task<object> GetAuthorsWithBookCountsAsync()
        {
            return _authorRepository.GetAllWithBookCountsAsync();
        }
    }
}
