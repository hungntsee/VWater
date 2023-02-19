using Microsoft.AspNetCore.Mvc;
using Service.Services;
using Service.Warehouses;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductInMenuController : ControllerBase
    {
        private IProductInMenuService _productInMenuService;
        public ProductInMenuController(IProductInMenuService productInMenuService)
        {
            _productInMenuService = productInMenuService;
        }

        // GET: api/<ProductInMenuController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var productInMenus = _productInMenuService.GetAll();
            return Ok(productInMenus);
        }

        // GET api/<ProductInMenuController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var productInMenu = _productInMenuService.GetById(id);
            return Ok(productInMenu);
        }

        // POST api/<ProductInMenuController>
        [HttpPost]
        public IActionResult Create([FromBody] ProductInMenuCreateModel request)
        {
            _productInMenuService.Create(request);
            return Ok(new { message = "Product In Menu created" });
        }

        // PUT api/<ProductInMenuController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductInMenuUpdateModel request)
        {
            _productInMenuService.Update(id, request);
            return Ok(new { message = "Product In Menu updated" });
        }

        // DELETE api/<ProductInMenuController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productInMenuService.Delete(id);
            return Ok(new { message = "Product In Menu deleted" });
        }
    }
}
