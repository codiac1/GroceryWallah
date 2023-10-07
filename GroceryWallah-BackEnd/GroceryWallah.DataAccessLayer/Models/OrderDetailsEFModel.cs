using System.ComponentModel.DataAnnotations;


namespace GroceryWallah.DataAccessLayer.Models
{
    public class OrderDetailsEFModel
    {
        [Key]
        public int Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
