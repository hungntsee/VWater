using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Data.Entities;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private IOrderDetailService _orderDetailService;

        public OrderDetailController (IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        // GET: api/<OrderDetailController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var orderDetails = _orderDetailService.GetAll();
            return Ok(orderDetails);
        }

        // GET api/<OrderDetailController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var orderDetail = _orderDetailService.GetById(id);
            return Ok(orderDetail);
        }

        // POST api/<OrderDetailController>
        [HttpPost]
        public IActionResult Create([FromBody] OrderDetailCreateModel request)
        {
            _orderDetailService.Create(request);
            return Ok(new { message = "Order Detail created" });
        }

        // PUT api/<OrderDetailController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderDetailUpdateModel request)
        {
            _orderDetailService.Update(id, request);
            return Ok(new { message = "Order Detail updated" });
        }

        // DELETE api/<OrderDetailController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _orderDetailService.Delete(id);
            return Ok(new { message = "Order Detail deleted" });
        }
    }
}
