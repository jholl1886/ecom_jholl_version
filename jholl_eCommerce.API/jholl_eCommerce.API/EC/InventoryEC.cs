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
            return new MSSQLContext().GetProducts().Select(p => new ProductDTO(p));
        }

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p) 
        {
            /*bool isAdd = false;
            var existingProduct = Filebase.Current.Products.FirstOrDefault(prod => prod.Id == p.Id);
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
                    p.Id = Filebase.Current.NextProductId;
                }
            }

            if (isAdd)
            {
                Filebase.Current.Products.Add(new Product(p));
            }*/

            if (p.Id == 0)
            {
                return new ProductDTO(new MSSQLContext().AddProduct(new Product(p)));
            }
            else
            {
                return new ProductDTO(new MSSQLContext().EditProduct(new Product(p)));
            }

        }

        public async Task<ProductDTO> Delete(int id)
        {
            return new ProductDTO(new MSSQLContext().Delete(id));
                
            //    Products.FirstOrDefault(p => p.Id == id);
            //if (productToDelete == null)
            //{
            //    return null;
            //}
            //Filebase.Current.Products.Remove(productToDelete);
            //return new ProductDTO(productToDelete); //when this becomes dto do this

            
        }

        public async Task<IEnumerable<ProductDTO>> Search(string? query)
        {
            return Filebase.Current.Products
            .Where(p => p.Name != null && 
             p.Name.ToUpper().Contains(query?.ToUpper() ?? string.Empty)  ||
              (p.Description != null && p.Description.ToUpper().Contains(query?.ToUpper() ?? string.Empty)))
            .Take(100)
            .Select(p => new ProductDTO(p));
        }

    }
}
