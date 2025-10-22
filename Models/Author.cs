using System.ComponentModel.DataAnnotations;

namespace quest5.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 1)]
        public string Name { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }
    }
}
