using GroceryWallah.DataAccessLayer.IRepository;
using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DataAccessLayer.Repositories;
using GroceryWallah.DTO;
using GroceryWallah.DTO.Mapper;
using Org.BouncyCastle.Asn1.X509;

namespace GroceryWallah.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllCartItemsAsync(Guid userId)
        {
            IEnumerable<Product> cartItems = await _cartRepository.GetCartItemAsync(userId);
            var allProductsOfUser = new List<ProductDto>();

            foreach (var item in cartItems)
            {
                allProductsOfUser.Add(ProductMapper.ToDTO(item));
            }
            return allProductsOfUser;
        }

        public async Task<CartDto> AddProductToCartAsync(Guid userId, Guid productId, int quantity)
        {
            Cart cartItem = await _cartRepository.GetCartItemsByUserIdAsync(userId, productId);

            if (cartItem != null)
            {
                // If the cart item already exists, update its quantity
                await _cartRepository.UpdateCartItemAsync(cartItem, cartItem.Quantity += quantity);
            }
            else
            {
                cartItem = new Cart
                {
                    CartId = new Guid(),
                    UserId = userId,
                    ProductId = productId,
                    Quantity = quantity
                };
                await _cartRepository.AddCartItemAsync(cartItem);
            }
            return CartMapper.ToDto(cartItem);
        }

        public async Task<bool> RemoveProductFromCartAsync(Guid userId, Guid productId)
        {
            Cart cartItem = await _cartRepository.GetCartItemsByUserIdAsync(userId, productId);
            if (cartItem != null)
            {
                await _cartRepository.RemoveCartItemAsync(cartItem);
                return true;
            }
            return false;
        }

        public async Task<CartDto?> UpdateQuantityOfProductAsync(Guid userId, Guid productId, int quantity)
        {
            Cart cartItem = await _cartRepository.GetCartItemsByUserIdAsync(userId, productId);
            if (cartItem != null)
            {
                await _cartRepository.UpdateCartItemAsync(cartItem, quantity);
                return CartMapper.ToDto(cartItem);
            }
            return null;
        }

        public async Task<string> PlaceOrder(Order order)
        {
            var orderId = Guid.NewGuid();
            var productsDTO = order.allProducts.Select(p => ProductMapper.ToModel(p)).ToList();
            await _cartRepository.PlaceOrder(orderId, order.UserId);
            await _cartRepository.SaveOrderDetails(orderId, productsDTO);
            return "ok";
        }

        public async Task<List<OrderDetailsDto>> MyOrders(Guid userId)
        {
            var orderDetails = await _cartRepository.MyOrders(userId);
            var res = orderDetails.Select(o => OrderDetailsMapper.ToDTO(o)).ToList();

            return res;
        }
    }
}
