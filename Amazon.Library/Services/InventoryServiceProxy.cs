using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Library.Utilities;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<Product> products;

        public ReadOnlyCollection<Product> Products
        {
            get
            {
                return products.AsReadOnly(); 
            }
        }

        private int NextId
        {
            get
            {
                if(!products.Any())
                {
                    return 1;
                }

                return products.Select(p => p.Id).Max() + 1;
            }
        }

        public Product AddOrUpdate(Product p)
        {
            bool isAdd = false;
            var existingProduct = products.FirstOrDefault(prod => prod.Id == p.Id);
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
                    p.Id = NextId;
                }
            }

            if(isAdd)
            {
                products.Add(p);
            }

            return p;
        }

        public void Delete(int id) //wasnt here from Mills' GITHUB
        {
            var productToDelete = products.FirstOrDefault(p => p.Id == id);
            if (products == null)
            {
                return;
            }
            products.Remove(productToDelete);
            //return productToDelete; //when this becomes dto do this
            
        }

        private InventoryServiceProxy()
        {
            
            

            //TODO add a webcall
            
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<Product>>(response);
        }

        public static InventoryServiceProxy Current
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new InventoryServiceProxy();
                    }
                }
                return instance;
            }
        }
    }
}
