using Amazon.Library.Models;
using eCommerce.Library.DTO;
using jholl_eCommerce.API.DataBase;

namespace jholl_eCommerce.API.EC
{
    public class InventoryEC
    {
        //cant be static cant be singleton
       
        public InventoryEC() { 
            
        }

        public async Task<IEnumerable<ProductDTO>> Get()
        {
            return FakeDatabase.Products.Take(100).Select(p => new ProductDTO(p));
        }

    }
}
