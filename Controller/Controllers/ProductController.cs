using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.GetAll();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _productService.GetById(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Create([FromBody] ProductCreateModel model)
        {
            _productService.Create(model);
            return Ok(new { message = "Product created" });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ProductUpdateModel model)
        {
            _productService.Update(id, model);
            return Ok(new { message = "Product updated" });
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.Delete(id);
            return Ok(new { message = "Product deleted" });
        }

        // GET api/<ProductController>/GetNumberOfArea
        [HttpGet("/GetNumberOfProduct")]
        public IActionResult GetNumberOfProduct()
        {
            return Ok(new { NumberOfProduct = _productService.GetNumberOfProduct() });
        }
    }
}
