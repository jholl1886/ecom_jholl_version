using Amazon.Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace jholl_eCommerce.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;
        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }


        [HttpGet()]
        public IEnumerable<Product> Get()
        {
            return new List<Product>{
                new Product{Id = 1,Name = "Product 1", Price=1.75M, Quantity=1, IsBogo = false}
                , new Product{Id = 2,Name = "Product 2", Price=10M, Quantity=10,IsBogo = false}
                , new Product{Id = 3,Name = "Product 3", Price=137.11M, Quantity=100, IsBogo = false}
            };
        }

    }
}
