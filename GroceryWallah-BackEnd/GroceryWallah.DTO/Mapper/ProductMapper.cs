using GroceryWallah.DataAccessLayer.Models;

namespace GroceryWallah.DTO.Mapper
{
    public static class ProductMapper
    {
        public static ProductDto ToDTO(this Product product)
        {
            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Quantity = product.Quantity,
                ImageLink = product.ImageLink,
                Price = product.Price,
                Discount = product.Discount,
                Specification = product.Specification
            };
        }

        public static Product ToModel(this ProductDto product)
        {
            return new Product
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category,
                Quantity = product.Quantity,
                ImageLink = product.ImageLink,
                Price = product.Price,
                Discount = product.Discount,
                Specification = product.Specification
            };
        }



    }
}
