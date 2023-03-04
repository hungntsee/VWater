using Microsoft.AspNetCore.Mvc;
using Service.DeliveryAddresses;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryAddressController : ControllerBase
    {
        private IDeliveryAddressService _deliveryAddressService;
        public DeliveryAddressController(IDeliveryAddressService deliveryAddressService)
        {
            _deliveryAddressService = deliveryAddressService;
        }

        // GET: api/<DeliveryAddressController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var deliveryAddresss = _deliveryAddressService.GetAll();
            return Ok(deliveryAddresss);
        }

        // GET api/<DeliveryAddressController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var deliveryAddress = _deliveryAddressService.GetById(id);
            return Ok(deliveryAddress);
        }

        // GET api/<DeliveryAddressController>/GetOrders
        [HttpGet("{id}")]
        public IActionResult GetOrdersByAddress(int id)
        {
            var deliveryAddress = _deliveryAddressService.GetOrdersByDeliveryAddress(id);
            return Ok(deliveryAddress);
        }

        // POST api/<DeliveryAddressController>
        [HttpPost]
        public IActionResult Create([FromBody] DeliveryAddressCreateModel request)
        {
            var deliveryAddress = _deliveryAddressService.Create(request);
            return Ok(new
            {
                message = "DeliveryAddress created",
                deliveryAddress
            });
        }

        // PUT api/<DeliveryAddressController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DeliveryAddressUpdateModel request)
        {
            _deliveryAddressService.Update(id, request);
            return Ok(new { message = "DeliveryAddress updated" });
        }

        // DELETE api/<DeliveryAddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _deliveryAddressService.Delete(id);
            return Ok(new { message = "DeliveryAddress deleted" });
        }
    }
}
