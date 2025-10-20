using System.ComponentModel.DataAnnotations;

namespace Task4Week3.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название книги обязательно")]
        public string? Title { get; set; }

        public int PublishedYear { get; set; }

        public int AuthorId { get; set; }
    }
}