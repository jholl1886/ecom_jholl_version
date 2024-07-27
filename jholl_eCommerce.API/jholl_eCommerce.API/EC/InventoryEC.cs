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

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            bool isAdd = false;
            var existingProduct = FakeDatabase.Products.FirstOrDefault(prod => prod.Id == p.Id);
            if (existingProduct != null)
            {
                existingProduct.Name = p.Name;
                existingProduct.Price = p.Price;
                existingProduct.Quantity = p.Quantity;
                existingProduct.IsBogo = p.IsBogo;
                existingProduct.MarkDown = p.MarkDown;
            }

            else
            {
                if (p.Id == 0)
                {
                    isAdd = true;
                    p.Id = FakeDatabase.NextProductId;
                }
            }

            if (isAdd)
            {
                FakeDatabase.Products.Add(new Product(p));
            }

            return p;
            
        }

        public async Task<ProductDTO> Delete(int id)
        {
            var productToDelete = FakeDatabase.Products.FirstOrDefault(p => p.Id == id);
            if (productToDelete == null)
            {
                return null;
            }
            FakeDatabase.Products.Remove(productToDelete);
            return new ProductDTO(productToDelete); //when this becomes dto do this
        }

        public async Task<IEnumerable<ProductDTO>> Search(string? query)
        {
            return FakeDatabase.Products
            .Where(p => p.Name != null && 
             p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty)  ||
              (p.Description != null && p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
            .Take(100)
            .Select(p => new ProductDTO(p));
        }

    }
}
