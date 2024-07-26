using Amazon.Library.Models;
using eCommerce.Library.DTO;
using jholl_eCommerce.API.EC;
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
        public async Task<IEnumerable<ProductDTO>> Get()
        {//only ever one line of code here ever
            return await new InventoryEC().Get();
        }

    }
}
