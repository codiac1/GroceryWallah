using GroceryWallah.DataAccessLayer.Data;
using GroceryWallah.DataAccessLayer.IRepository;
using GroceryWallah.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;


namespace GroceryWallah.DataAccessLayer.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;

        public ProductRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbContext.Products.Where(p => p.IsDeleted == false).ToList();
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetProductById(Guid id)
        {
            var product = await _dbContext.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<bool> RemoveProduct(Guid productId)
        {
            var product = await GetProductById(productId);
            if (product == null)
                return false;

            product.IsDeleted = true ;
            await UpdateProduct(product);
            return true;
        }
    }
}
