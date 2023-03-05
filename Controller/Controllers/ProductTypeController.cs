using Microsoft.AspNetCore.Mvc;
using Service.ProductTypes;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        private IProductTypeService _productTypeService;
        public ProductTypeController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        // GET: api/<ProductTypeController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var productTypes = _productTypeService.GetAll();
            return Ok(productTypes);
        }

        // GET api/<ProductTypeController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var productType = _productTypeService.GetById(id);
            return Ok(productType);
        }

        // POST api/<ProductTypeController>
        [HttpPost]
        public IActionResult Create([FromBody] ProductTypeCreateModel request)
        {
            _productTypeService.Create(request);
            return Ok(new { message = "ProductType created" });
        }

        // PUT api/<ProductTypeController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductTypeUpdateModel request)
        {
            _productTypeService.Update(id, request);
            return Ok(new { message = "ProductType updated" });
        }

        // DELETE api/<ProductTypeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productTypeService.Delete(id);
            return Ok(new { message = "ProductType deleted" });
        }
    }
}
