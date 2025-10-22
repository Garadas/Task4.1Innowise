using System.ComponentModel.DataAnnotations;

namespace quest5.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [Range(0, 3000)]
        public int? PublishedYear { get; set; }

        [Required]
        public int AuthorId { get; set; }
    }
}
