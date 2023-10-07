using System.ComponentModel.DataAnnotations;

namespace GroceryWallah.DataAccessLayer.Models
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [Range(0, 10)]
        public int Quantity { get; set; }

        [Required]
        [Url]
        public string ImageLink { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, double.MaxValue)]
        public decimal? Discount { get; set; }

        [StringLength(100)]
        public string Specification { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

}
