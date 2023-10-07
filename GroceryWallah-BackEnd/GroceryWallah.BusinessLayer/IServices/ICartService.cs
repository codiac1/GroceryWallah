using GroceryWallah.DTO;

namespace GroceryWallah.Services
{
    public interface ICartService
    {
        Task<IEnumerable<ProductDto>> GetAllCartItemsAsync(Guid userId);
        Task<CartDto> AddProductToCartAsync(Guid userId, Guid productId, int quantity);
        Task<bool> RemoveProductFromCartAsync(Guid userId, Guid productId);
        Task<CartDto> UpdateQuantityOfProductAsync(Guid userId, Guid productId, int quantity);
        Task<string> PlaceOrder(Order order);
        Task<List<OrderDetailsDto>> MyOrders(Guid userId);
    }
}
