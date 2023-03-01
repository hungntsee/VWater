using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseBaselineController : ControllerBase
    {
        private IWarehouseBaselineService _warehouseBaselineService;
        public WarehouseBaselineController(IWarehouseBaselineService warehouseBaselineService)
        {
            _warehouseBaselineService = warehouseBaselineService;
        }

        // GET: api/<WarehouseBaselineController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var warehouseBaselines = _warehouseBaselineService.GetAll();
            return Ok(warehouseBaselines);
        }

        // GET api/<WarehouseBaselineController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var warehouseBaseline = _warehouseBaselineService.GetById(id);
            return Ok(warehouseBaseline);
        }

        // POST api/<WarehouseBaselineController>
        [HttpPost]
        public IActionResult Create([FromBody] WarehouseBaselineCreateModel request)
        {
            _warehouseBaselineService.Create(request);
            return Ok(new { message = "WarehouseBaseline created" });
        }

        // PUT api/<WarehouseBaselineController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] WarehouseBaselineUpdateModel request)
        {
            _warehouseBaselineService.Update(id, request);
            return Ok(new { message = "WarehouseBaseline updated" });
        }

        // DELETE api/<WarehouseBaselineController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _warehouseBaselineService.Delete(id);
            return Ok(new { message = "WarehouseBaseline deleted" });
        }
    }
}
