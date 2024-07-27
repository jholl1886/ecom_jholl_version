using Amazon.Library.Models;
using eCommerce.Library.DTO;
using jholl_eCommerce.API.EC;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

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

        [HttpDelete("/{id}")]
        public async Task<ProductDTO> Delete(int id)
        {
            return await new InventoryEC().Delete(id);
        }

        [HttpPost]
        public async Task<ProductDTO> AddOrUpdate([FromBody] ProductDTO p)
        {
            return await new InventoryEC().AddOrUpdate(p);
        }

        [HttpPost("Search")]
        public async Task<IEnumerable<ProductDTO>> Get(Query query)
        {
            return await new InventoryEC().Search(query.QueryString);
        }

    }
}
