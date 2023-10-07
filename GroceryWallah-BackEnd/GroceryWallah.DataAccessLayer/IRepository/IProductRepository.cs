using GroceryWallah.DataAccessLayer.Models;

namespace GroceryWallah.DataAccessLayer.IRepository
{

    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Task<Product> AddProductAsync(Product product);
        Task<Product> GetProductById(Guid id);
        Task<Product> UpdateProduct(Product product);
        Task<bool> RemoveProduct(Guid productId);
    }
}
