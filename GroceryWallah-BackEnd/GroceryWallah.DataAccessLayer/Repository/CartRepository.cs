using GroceryWallah.DataAccessLayer.Data;
using GroceryWallah.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace GroceryWallah.DataAccessLayer.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _dbContext;

        public CartRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Cart> GetCartItemsByUserIdAsync(Guid userId, Guid productId)
        {
            return _dbContext.CartItems.FirstOrDefault(c => c.UserId == userId && c.ProductId == productId);
        }

        public async Task<IEnumerable<Product>> GetCartItemAsync(Guid userId)
        {
            var products = from cart in _dbContext.CartItems
                           join product in _dbContext.Products on cart.ProductId equals product.ProductId
                           where cart.UserId == userId
                           select new Product
                           {
                               ProductId = product.ProductId,
                               Name = product.Name,
                               Description = product.Description,
                               Quantity = cart.Quantity,
                               Category = product.Category,
                               Price = product.Price,
                               ImageLink = product.ImageLink,
                               Discount = product.Discount,
                               Specification = product.Specification
                           };

            return await products.ToListAsync();
        }

        public async Task<Cart> AddCartItemAsync(Cart cartItem)
        {
            await _dbContext.CartItems.AddAsync(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<bool> RemoveCartItemAsync(Cart cartItem)
        {
            _dbContext.CartItems.Remove(cartItem);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Cart> UpdateCartItemAsync(Cart cartItem, int quantity)
        {
            cartItem.Quantity = quantity;
            _dbContext.CartItems.Update(cartItem);
            await _dbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<string> PlaceOrder(Guid orderId, Guid userId)
        {
            var order = new OrderEFModel
            {
                OrderId = orderId,
                UserId = userId,
                Status = "Confirmed",
                Date = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")

            };
            await _dbContext.Orders.AddAsync(order);
            return "Order Placed";
        }

        public async Task<string> SaveOrderDetails(Guid orderId, List<Product> OrderedProducts)
        {
            foreach (var productobj in OrderedProducts)
            {
                var orderDetails = new OrderDetailsEFModel
                {
                    OrderId = orderId,
                    ProductId = productobj.ProductId,
                    Quantity = productobj.Quantity // quantity of item that is ordered 
                };
                await _dbContext.OrdersDetails.AddAsync(orderDetails);
                // Update the product inventory by deducting the ordered quantity
                var existingProduct = await _dbContext.Products.FindAsync(productobj.ProductId);
                if (existingProduct != null)
                {
                    existingProduct.Quantity -= productobj.Quantity;
                }

                await _dbContext.SaveChangesAsync();
            }
            return "Save Successful";
        }
        public async Task<List<OrderDetails>> MyOrders(Guid userId)
        {
            var products = await (from orders in _dbContext.Orders
                                  join orderDetails in _dbContext.OrdersDetails on orders.OrderId equals orderDetails.OrderId
                                  join product in _dbContext.Products on orderDetails.ProductId equals product.ProductId
                                  where orders.UserId == userId
                                  select new OrderDetails
                                  {
                                      OrderId = orders.OrderId,
                                      ProductName = product.Name,
                                      ProductImage = product.ImageLink,
                                      Quantity = orderDetails.Quantity,
                                      Date = orders.Date
                                  }).ToListAsync();
            return products;
        }
    }
}
