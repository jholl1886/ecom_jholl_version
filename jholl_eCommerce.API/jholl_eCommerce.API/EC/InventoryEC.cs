using Amazon.Library.Models;
using jholl_eCommerce.API.DataBase;

namespace jholl_eCommerce.API.EC
{
    public class InventoryEC
    {
        //cant be static cant be singleton
       
        public InventoryEC() { 
            
        }

        public async Task<IEnumerable<Product>> Get()
        {
            return FakeDatabase.Products;
        }

    }
}
