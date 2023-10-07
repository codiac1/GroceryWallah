using GroceryWallah.DTO;
using GroceryWallah.Services;
using Microsoft.AspNetCore.Mvc;

namespace GroceryWallah.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("GetItems")]
        public async Task<IEnumerable<ProductDto>> GetAllCartItemsAsync([FromBody] CartDto cartRow)
        { 
            try
            {
               
                var cartItems = await _cartService.GetAllCartItemsAsync(cartRow.UserId);
                return cartItems;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching cart items.", ex);
            }
        }


        [HttpPost("addProduct")]
        public async Task<ActionResult<CartDto>> AddProductToCart([FromBody] CartDto newCart)
        {
            try
            {
                var addedItem = await _cartService.AddProductToCartAsync(newCart.UserId, newCart.ProductId, newCart.Quantity);
                return Ok(addedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("deleteItem")]
        public async Task<ActionResult<bool>> RemoveProductFromCartAsync([FromBody] CartDto cartRow)
        {
            try
            {
                bool status = await _cartService.RemoveProductFromCartAsync(cartRow.UserId, cartRow.ProductId);
                return Ok(status);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("UpdateQuantity")]
        public async Task<ActionResult<CartDto>> UpdateQuantityOfProductAsync([FromBody] CartDto cartRow)
        {
            try
            {
                var updatedItem = await _cartService.UpdateQuantityOfProductAsync(cartRow.UserId, cartRow.ProductId, cartRow.Quantity);
                return Ok(updatedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("/cart/placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] Order order)
        {
            await _cartService.PlaceOrder(order);
            return Ok("Order Confirmed");
        }

        [HttpGet]
        public async Task<IActionResult> MyOrders([FromQuery] Guid userId)
        {
            var orders = await _cartService.MyOrders(userId);
            return Ok(orders);
        }
    }
}
