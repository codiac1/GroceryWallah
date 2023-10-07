using GroceryWallah.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryWallah.DTO.Mapper
{
    public static class OrderDetailsMapper
    {
        public static OrderDetailsDto ToDTO(this OrderDetails od)
        {
            return new OrderDetailsDto
            {
                OrderId = od.OrderId,
                ProductName = od.ProductName,
                ProductImage = od.ProductImage,
                Quantity = od.Quantity,
                Date = od.Date
            };
        }

        public static OrderDetails ToModel(this OrderDetailsDto od)
        {
            return new OrderDetails
            {
                OrderId = od.OrderId,
                ProductName = od.ProductName,
                ProductImage = od.ProductImage,
                Quantity = od.Quantity,
                Date = od.Date
            };
        }


    }
}
