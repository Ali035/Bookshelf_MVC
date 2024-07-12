using System.ComponentModel.DataAnnotations;

namespace Bookshelf.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public required string Name { get; set; }
        [Required]
        [Range(0, 100)]
        public int DisplayOrder { get; set; }
    }
}
