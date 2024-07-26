using Amazon.Library.Models;

namespace jholl_eCommerce.API.DataBase
{
    public static class FakeDatabase
    {
        public static int NextProductId
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
        public static List<Product> Products { get; } = new List<Product>{
                new Product{Id = 1,Name = "Product 1", Price=1.75M, Quantity=1, IsBogo = false}
                , new Product{Id = 2,Name = "Product 2", Price=10M, Quantity=10,IsBogo = false}
                , new Product{Id = 3,Name = "Product 3", Price=137.11M, Quantity=100, IsBogo = false}
                ,new Product{Id = 4,Name = "Product 4",Price = 420.69M,Quantity = 25,IsBogo = false}
            };
    }
}
