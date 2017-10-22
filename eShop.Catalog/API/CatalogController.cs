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
        [Route("[action")]
        public IEnumerable<string> Get()
        {
            try
            {
                //Service
                IEnumerable<Product> products = await _repository.GetCatalogAsync();

                if (products.ToList().Count != 0) return Ok(model);

                var errorMessage = new ErrorMessage { Message = "NotFound" };
                return Negotiate.WithModel(errorMessage).WithStatusCode(HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                _logger.Debug(ex.Message);
                var errorMessage = new ErrorMessage { Message = "InternalServerError" };
                return Negotiate.WithModel(errorMessage).WithStatusCode(HttpStatusCode.InternalServerError);
            }
        }

        // GET api/products/5
        [HttpGet]
        [Route("items/{id:int}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/products
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
    }
}
