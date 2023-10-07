using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWallah.DTO
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string ImageLink { get; set; }
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        public string Specification { get; set; }
        public bool IsDeleted { get; set; }
    }
}
