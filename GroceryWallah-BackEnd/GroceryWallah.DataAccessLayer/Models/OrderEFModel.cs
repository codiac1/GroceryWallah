using System.ComponentModel.DataAnnotations;


namespace GroceryWallah.DataAccessLayer.Models
{
    public class OrderEFModel
    {
        [Key]
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
    }
}
