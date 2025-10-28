using System.ComponentModel.DataAnnotations;

namespace Task4Week4.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(70, ErrorMessage = "Имя не может быть длиннее 70 символов")]
        public string? Name { get; set; }

        public DateTime DateOfBirth { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}