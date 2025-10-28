using Microsoft.EntityFrameworkCore;
using Task4Week4.Models;

namespace Task4Week4.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().HasData(
                new Author { Id = 1, Name = "Лев Толстой", DateOfBirth = new DateTime(1828, 9, 9) },
                new Author { Id = 2, Name = "Фёдор Достоевский", DateOfBirth = new DateTime(1821, 11, 11) },
                new Author { Id = 3, Name = "Дж.К. Роулинг", DateOfBirth = new DateTime(1965, 7, 31) }
            );

            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Война и мир", PublishedYear = 1869, AuthorId = 1 },
                new Book { Id = 2, Title = "Преступление и наказание", PublishedYear = 1866, AuthorId = 2 },
                new Book { Id = 3, Title = "Гарри Поттер и философский камень", PublishedYear = 1997, AuthorId = 3},
                new Book { Id = 4, Title = "Гарри Поттер и Тайная комната", PublishedYear = 1998, AuthorId = 3}
            );
        }
    }
}

