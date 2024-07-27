using Amazon.Library.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Library.Utilities;
using eCommerce.Library.DTO;

namespace Amazon.Library.Services
{
    public class InventoryServiceProxy
    {
        private static InventoryServiceProxy? instance;
        private static object instanceLock = new object();

        private List<ProductDTO> products;

        public ReadOnlyCollection<ProductDTO> Products
        {
            get
            {
                return products.AsReadOnly(); 
            }
        }

        public async Task<IEnumerable<ProductDTO>> Get() //this is what fixed UI not updating after an add
        {
            var result = await new WebRequestHandler().Get("/Inventory");
            var deserializedResult = JsonConvert.DeserializeObject<List<ProductDTO>>(result);

            products = deserializedResult?.ToList() ?? new List<ProductDTO>();
            return products;
        }

      

        public async Task<ProductDTO> AddOrUpdate(ProductDTO p)
        {
            var result = await new WebRequestHandler().Post("/Inventory",p);
            return JsonConvert.DeserializeObject<ProductDTO>(result);
        }

        public async Task<ProductDTO>? Delete(int id) //wasnt here from Mills' GITHUB
        {
            var response = await new WebRequestHandler().Delete($"/{id}");
            var productToDelete = JsonConvert.DeserializeObject<ProductDTO>(response);
            return productToDelete; //when this becomes dto do this
            
        }

        private InventoryServiceProxy()
        {
            var response = new WebRequestHandler().Get("/Inventory").Result;
            products = JsonConvert.DeserializeObject<List<ProductDTO>>(response);
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
