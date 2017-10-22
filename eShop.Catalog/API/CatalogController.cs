using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using eShop.Catalog.Domain;
using eShop.Catalog.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace eShop.Catalog.API
{
    [Route("api/v1/[controller]")]
    public class CatalogController : Controller
    {
        private readonly ICatalogRepository _repository;

        public CatalogController(ICatalogRepository repository)
        {
            _repository = repository;
        }

        // GET api/v1/[controller]/items[?pageSize=3&pageIndex=10]
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Items([FromQuery]int pageSize = 10, [FromQuery]int pageIndex = 0)
        {
             var result = await _repository.GetItemsAsync(pageIndex, pageSize);
             var model = new PaginatedItemsViewModel<CatalogItem>(pageIndex, pageSize, result.TotalItems, result.ItemsOnPage);

             return Ok(model);
        }

        // GET api/items/5
        [HttpGet]
        [Route("items/{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/items
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/items/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // PUT api/items/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
