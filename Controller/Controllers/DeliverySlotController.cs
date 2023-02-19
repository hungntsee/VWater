using Microsoft.AspNetCore.Mvc;
using Service.DeliverySlots;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverySlotController : ControllerBase
    {
        private IDeliverySlotService _deliverySlotService;
        public DeliverySlotController(IDeliverySlotService deliverySlotService)
        {
            _deliverySlotService = deliverySlotService;
        }

        // GET: api/<DeliverySlotController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var deliverySlots = _deliverySlotService.GetAll();
            return Ok(deliverySlots);
        }

        // GET api/<DeliverySlotController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var deliverySlot = _deliverySlotService.GetById(id);
            return Ok(deliverySlot);
        }

        // POST api/<DeliverySlotController>
        [HttpPost]
        public IActionResult Create([FromBody] DeliverySlotCreateModel request)
        {
            _deliverySlotService.Create(request);
            return Ok(new { message = "DeliverySlot created" });
        }

        // PUT api/<DeliverySlotController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DeliverySlotUpdateModel request)
        {
            _deliverySlotService.Update(id, request);
            return Ok(new { message = "DeliverySlot updated" });
        }

        // DELETE api/<DeliverySlotController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _deliverySlotService.Delete(id);
            return Ok(new { message = "DeliverySlot deleted" });
        }
    }
}
