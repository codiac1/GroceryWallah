using GroceryWallah.BusinessLayer.IServices;
using GroceryWallah.DataAccessLayer.Models;
using GroceryWallah.DTO;
using Microsoft.AspNetCore.Mvc;

namespace GroceryWallah.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductListController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductListController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet]
        public IActionResult GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductDto product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productDto = await _productService.AddProductAsync(product);

            return Ok(productDto); // Return the added product as a response
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductById(id);

                if (product == null)
                {
                    return NotFound();
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update")]
        public async Task<ActionResult<ProductDto>> UpdateProduct([FromBody] ProductDto product)
        {
            var updatedProduct = await _productService.UpdateProduct(product);
            return Ok(updatedProduct);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> RemoveProduct([FromBody]ProductDto product)
        {
            var success = await _productService.RemoveProduct(product.ProductId);
            if (!success)
                return NotFound();

            return NoContent();
        }

    }
}