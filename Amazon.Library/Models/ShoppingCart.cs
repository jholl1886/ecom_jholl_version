using eCommerce.Library.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Library.Models
{
    public class ShoppingCart
    {
        public int Id { get; set; }

        public decimal Price { get; set; } = 0;

        
        public List<ProductDTO> Contents { get; set; } = new List<ProductDTO>();
    }
}
