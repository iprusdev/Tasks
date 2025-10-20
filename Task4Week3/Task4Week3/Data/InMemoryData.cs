using Task4Week3.Models;

namespace Task4Week3.Data
{
    public static class InMemoryData
    {
        public static readonly List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Name = "Лев Толстой", DateOfBirth = new DateTime(1828, 9, 9) },
            new Author { Id = 2, Name = "Фёдор Достоевский", DateOfBirth = new DateTime(1821, 11, 11) },
            new Author { Id = 3, Name = "Дж.К. Роулинг", DateOfBirth = new DateTime(1965, 7, 31)}
        };

        public static readonly List<Book> Books = new List<Book>
        {
            new Book { Id = 1, Title = "Война и мир", PublishedYear = 1869, AuthorId = 1 },
            new Book { Id = 2, Title = "Преступление и наказание", PublishedYear = 1866, AuthorId = 2 },
            new Book { Id = 3, Title = "Гарри Поттер и философский камень", PublishedYear = 1997, AuthorId = 3},
            new Book { Id = 4, Title = "Гарри Поттер и Тайная комната", PublishedYear = 1998, AuthorId = 3}
        };
    }
}