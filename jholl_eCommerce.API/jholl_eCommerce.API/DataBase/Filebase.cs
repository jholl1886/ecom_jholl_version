using Amazon.Library.Models;
using Newtonsoft.Json;

namespace jholl_eCommerce.API.DataBase
{
    public class Filebase
    {
        private string _root;
        private static Filebase _instance;


        public static Filebase Current
        { 
            get
            {
                if(_instance == null)
                {
                    _instance = new Filebase();
                }

                return _instance;
            }
        }

        private Filebase()
        {
            _root = @"C:\temp\Products";
            
        }

        public Product AddOrUpdate(Product p)
        {
            //set up a new Id if one doesn't already exist
            if(p.Id <= 0)
            {
                p.Id = NextProductId;
            }

            //go to the right place]
            string path = $"{_root}\\{p.Id}.json";
            

            //if the item has been previously persisted
            if(File.Exists(path))
            {
                //blow it up
                File.Delete(path);
            }

            //write the file
            File.WriteAllText(path, JsonConvert.SerializeObject(p)); //not best practice

            //return the item, which now has an id
            return p;
        }

        public int NextProductId
        {
            get
            {
                if (!Products.Any())
                {
                    return 1;
                }

                return Products.Select(p => p.Id).Max() + 1;
            }
        }

        

        public List<Product> Products
        {
            get
            {
                var root = new DirectoryInfo(_root);
                var prods = new List<Product>();
                foreach (var appFile in root.GetFiles())
                {
                    var prod = JsonConvert.DeserializeObject<Product>(File.ReadAllText(appFile.FullName));
                    if(prod != null)
                    {
                        prods.Add(prod);
                    }
                    
                }
                return prods;
            }
        }

        public Product Delete(int id)
        {
            //go to the right place]
            string path = Path.Combine(_root, $"{id}.json");


            if (File.Exists(path))
            {
                // Read the product details before deleting (if needed for return)
                var product = JsonConvert.DeserializeObject<Product>(File.ReadAllText(path));

                // Delete the file
                File.Delete(path);

                // Return the deleted product
                return product;

            }

            // If the file doesn't exist, return null or throw an exception
            // or throw new FileNotFoundException($"Product with ID {id} not found.");
            
            throw new NotFiniteNumberException();
            
            


        }
    }


    
   

    
}
