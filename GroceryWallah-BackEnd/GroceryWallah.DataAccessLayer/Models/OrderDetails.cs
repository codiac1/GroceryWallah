namespace GroceryWallah.DataAccessLayer.Models
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get;  set; }
        public int Quantity { get;  set; }
        public string Date { get; set; }

    }
}
