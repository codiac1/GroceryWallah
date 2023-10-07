using System;
using System.Collections.Generic;
using GroceryWallah.DataAccessLayer.Models;

namespace GroceryWallah.DataAccessLayer.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartItemsByUserIdAsync(Guid userId, Guid productId);
        Task<IEnumerable<Product>> GetCartItemAsync(Guid userId);
        Task<Cart> AddCartItemAsync(Cart cartItem);
        Task<bool> RemoveCartItemAsync(Cart cartItem);
        Task<Cart> UpdateCartItemAsync(Cart cartItem, int quantity);
        Task<string> PlaceOrder(Guid orderId, Guid userId);
        Task<string> SaveOrderDetails(Guid orderId, List<Product> products);
        Task<List<OrderDetails>> MyOrders(Guid userId);
    }
}
