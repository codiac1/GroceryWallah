namespace GroceryWallah.DTO
{
    public class CartDto
    {
        public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
