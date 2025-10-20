using System.ComponentModel.DataAnnotations;

namespace Task4Week3.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Имя автора обязательно")]
        [StringLength(100, ErrorMessage = "Имя не может быть длиннее 100 символов")]
        public string? Name { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}