using System;
using System.Net;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace eShop.Catalog.API
{
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {
        private readonly ICatalogRepository _repository;
        private readonly ILogger _logger;

        public CatalogController(ICatalogRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet("[action]")]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<CatalogItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
             var result = await _repository.GetItemsAsync(pageIndex, pageSize);

             var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, result.TotalItems, result.ItemsOnPage);

             return Ok(model);
        }

        // GET api/v1/[controller]/items/5
        [HttpGet("items/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(CatalogItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(int id)
        {
            if (id <= 0) return BadRequest();

            var item = await _repository.GetItemAsync(id);
            
            if(item != null) return Ok(item);

            return NotFound();
        }

        // POST api/v1/[controller]/items
        [HttpPost("items")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProduct([FromBody]CatalogItem product)
        {
            try
            {
                if (await _repository.AddItemAsync(product) != true) return BadRequest();
                
                return CreatedAtAction(nameof(GetById), new {id = product.Id}, null);
            }
            catch(Exception ex)
            {
                _logger.Debug(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        // DELETE api/v1/[controller]/items
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _repository.DeleteItemAsync(id) == false) return NotFound();

            return NoContent();
        }

        // PUT api/v1/[controller]/items
        [HttpPut("items")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Put([FromBody]CatalogItem product)
        {

            if (await _repository.UpdateItemAsync(product) != true) return NotFound();

            return CreatedAtAction(nameof(GetById), new {id = product.Id}, null);
        }
    }
}
