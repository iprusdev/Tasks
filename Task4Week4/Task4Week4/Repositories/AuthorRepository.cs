using Microsoft.EntityFrameworkCore;
using Task4Week4.Data;
using Task4Week4.Models;

namespace Task4Week4.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly LibraryContext _context;

        public AuthorRepository(LibraryContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task UpdateAsync(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Authors.AnyAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Author>> FindAsync(string query)
        {
            return await _context.Authors
                .Where(a => a.Name != null && a.Name.Contains(query))
                .ToListAsync();
        }

        public async Task<object> GetAllWithBookCountsAsync()
        {
            return await _context.Authors
                .Select(a => new
                {
                    AuthorId = a.Id,
                    AuthorName = a.Name,
                    BookCount = a.Books.Count
                })
                .ToListAsync();
        }
    }
}

