using GroceryWallah.DataAccessLayer.Models;


namespace GroceryWallah.DTO.Mapper
{
    public static class CartMapper
    {
        public static CartDto ToDto(this Cart cart)
        {
            return new CartDto
            {
                CartId = cart.CartId,
                UserId = cart.UserId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity
            };
        }

        public static Cart ToModel(this CartDto cartDto)
        {
            return new Cart
            {
                CartId = cartDto.CartId,
                UserId = cartDto.UserId,
                ProductId = cartDto.ProductId,
                Quantity = cartDto.Quantity
            };
        }
    }
}
