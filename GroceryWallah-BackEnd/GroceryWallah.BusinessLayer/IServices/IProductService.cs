using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DTO;

namespace GroceryWallah.BusinessLayer.IServices
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetAllProducts();
        Task<ProductDto> AddProductAsync(ProductDto productDto);
        Task<ProductDto> GetProductById(Guid id);
        Task<ProductDto?> UpdateProduct(ProductDto productDto);
        Task<bool> RemoveProduct(Guid productId);
    }
}
