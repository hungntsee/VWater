using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var products = _orderService.GetAll();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _orderService.GetById(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateModel model)
        {
            _orderService.Create(model);
            return Ok(new { message = "Order created" });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderUpdateModel model)
        {
            _orderService.Update(id, model);
            return Ok(new { message = "Order updated" });
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok(new { message = "Order deleted" });
        }
    }
}
