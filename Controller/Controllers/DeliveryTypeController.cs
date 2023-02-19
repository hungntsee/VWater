using Microsoft.AspNetCore.Mvc;
using Service.DeliveryTypes;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryTypeController : ControllerBase
    {
        private IDeliveryTypeService _deliveryTypeService;
        public DeliveryTypeController(IDeliveryTypeService deliveryTypeService)
        {
            _deliveryTypeService = deliveryTypeService;
        }

        // GET: api/<DeliveryTypeController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var deliveryTypes = _deliveryTypeService.GetAll();
            return Ok(deliveryTypes);
        }

        // GET api/<DeliveryTypeController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var deliveryType = _deliveryTypeService.GetById(id);
            return Ok(deliveryType);
        }

        // POST api/<DeliveryTypeController>
        [HttpPost("register")]
        public IActionResult Create(DeliveryTypeCreateModel request)
        {
            _deliveryTypeService.Create(request);
            return Ok(new { message = "DeliveryType created" });
        }

        // PUT api/<DeliveryTypeController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, DeliveryTypeUpdateModel request)
        {
            _deliveryTypeService.Update(id, request);
            return Ok(new { message = "DeliveryType updated" });
        }

        // DELETE api/<DeliveryTypeController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _deliveryTypeService.Delete(id);
            return Ok(new { message = "DeliveryType deleted" });
        }
    }
}
