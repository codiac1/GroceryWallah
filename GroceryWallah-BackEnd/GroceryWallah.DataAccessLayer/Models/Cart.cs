using GroceryWallah.DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Cart
{
    [Key]
    public Guid CartId { get; set; }

    [Required]
    [ForeignKey("User")]
    public Guid UserId { get; set; }
    public virtual User User { get; set; }

    [Required]
    [ForeignKey("Product")]
    public Guid ProductId { get; set; }
    public virtual Product Product { get; set; }

    [Required]
    [Range(1, 10)]
    public int Quantity { get; set; }
}
