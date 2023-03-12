using Microsoft.AspNetCore.Mvc;
using Service.Services;
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

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _orderService.GetAll();
            return Ok(products);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _orderService.GetById(id);
            return Ok(product);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Create([FromBody] OrderCreateModel model)
        {
            var order = _orderService.Create(model);
            return Ok(new 
            { 
                message = "Order created",
                order
            });
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderUpdateModel model)
        {
            _orderService.Update(id, model);
            return Ok(new { message = "Order updated" });
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderService.Delete(id);
            return Ok(new { message = "Order deleted" });
        }
    }
}
