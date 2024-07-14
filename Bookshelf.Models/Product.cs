using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bookshelf.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        [Range(1, 10000)]
        public int ListPrice { get; set; }
        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for 1-50")]
        public int Price { get; set; }
        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for +50")]
        public int Price50 { get; set; }
        [Required]
        [Range(1, 10000)]
        [DisplayName("Price for +100")]
        public int Price100 { get; set; }
        [ForeignKey("CategoryId")]
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
