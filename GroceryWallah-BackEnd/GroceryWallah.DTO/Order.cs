
namespace GroceryWallah.DTO
{
    public class Order
    {
        public Guid UserId { get; set; }

        public IEnumerable<ProductDto> allProducts { get; set; }
    }
}
