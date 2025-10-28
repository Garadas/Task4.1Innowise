using System.ComponentModel.DataAnnotations;

namespace quest5.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
