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

        //GET api/<OrderController>/LastestOrder
        [HttpGet("/api/LastestOrder")]
        public IActionResult GetLatestOrder(int customer_id)
        {
            var order = _orderService.GetLastestOrder(customer_id);
            return Ok(order);
        }

        //GET api/<OrderController>/GetOrderByCustomer
        [HttpGet("/api/GetOrderByCustomer")]
        public IActionResult GetOrderForCustomer(int customer_id)
        {
            var orders = _orderService.GetOrderByCustomer(customer_id);
            return Ok(orders);
        }

        [HttpGet("/api/TakeOrder")]
        public IActionResult TakeOrder(int order_id, int shipper_id)
        {
            var order = _orderService.TakeOrder(order_id, shipper_id);
            return Ok(order);
        }

        [HttpGet("/api/FollowOrder")]
        public IActionResult FollowOrder(int customer_id)
        {
            var orders = _orderService.FollowOrder(customer_id);
            return Ok(orders);
        }

        [HttpGet("/api/ReOrder/{order_id}")]
        public IActionResult ReOrder(int order_id)
        {
            var order = _orderService.ReOrder(order_id);
            return Ok(order);
        }

        [HttpGet("/api/GetNumberOfOrder")]
        public IActionResult GetNumberOfOrder()
        {
            return Ok(new {numberOfOrder = _orderService.GetNumberOfOrder()});
        }

        [HttpGet("/api/GetReport")]
        public IActionResult GetReport()
        {
            return Ok(_orderService.GetReport());
        }

        [HttpPost("/api/CreateDepositNote")]
        public IActionResult CreateDepositNote([FromBody] DepositNoteCreateModel model)
        {
            var depositNote = _orderService.CreateDepositeNote(model);
            return Ok(depositNote);
        }
    }
}
