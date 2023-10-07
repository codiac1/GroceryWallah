using GroceryWallah.BusinessLayer.IServices;
using GroceryWallah.DataAccessLayer.IRepository;
using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DTO;
using GroceryWallah.DTO.Mapper;


namespace GroceryWallah.BusinessLayer.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<ProductDto> GetAllProducts()
        {
            var allProducts =  _productRepository.GetAllProducts();
            var allProductsDto = new List<ProductDto>();

            foreach (var product in allProducts)
            {
                allProductsDto.Add(ProductMapper.ToDTO(product));
            }

            return allProductsDto;
        }

        public async Task<ProductDto> AddProductAsync(ProductDto productDto)
        {
            productDto.ProductId = Guid.NewGuid();
            var product = ProductMapper.ToModel(productDto);
            var addedProduct = await _productRepository.AddProductAsync(product);
            var addedProductDto = ProductMapper.ToDTO(addedProduct);
            return addedProductDto;
        }

        public async Task<ProductDto> GetProductById(Guid id)
        {
            var product = await _productRepository.GetProductById(id);
            return ProductMapper.ToDTO(product);
        }

        public async Task<ProductDto?> UpdateProduct(ProductDto productDto)
        {
            var product = ProductMapper.ToModel(productDto);
            var existingProduct = await _productRepository.GetProductById(product.ProductId);

            if (existingProduct == null)
                return null;

            existingProduct.Name = productDto.Name;
            existingProduct.Price = productDto.Price;
            existingProduct.Description = productDto.Description;
            existingProduct.Category = productDto.Category;
            existingProduct.Specification = productDto.Specification;
            existingProduct.Discount = productDto.Discount;
            existingProduct.Quantity = productDto.Quantity;
            existingProduct.ImageLink = productDto.ImageLink;

            var productRes = await _productRepository.UpdateProduct(existingProduct);
            return ProductMapper.ToDTO(productRes);
        }

        public async Task<bool> RemoveProduct(Guid productId)
        {
            return await _productRepository.RemoveProduct(productId);
        }
    }
}
