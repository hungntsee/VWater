using Microsoft.AspNetCore.Mvc;
using Service.Warehouses;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // GET: api/<WarehouseController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var warehouses = _warehouseService.GetAll();
            return Ok(warehouses);
        }

        // GET api/<WarehouseController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var warehouse = _warehouseService.GetById(id);
            return Ok(warehouse);
        }

        // POST api/<WarehouseController>
        [HttpPost("register")]
        public IActionResult Create(WarehouseCreateModel request)
        {
            _warehouseService.Create(request);
            return Ok(new { message = "Warehouse created" });
        }

        // PUT api/<WarehouseController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, WarehouseUpdateModel request)
        {
            _warehouseService.Update(id, request);
            return Ok(new { message = "Warehouse updated" });
        }

        // DELETE api/<WarehouseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _warehouseService.Delete(id);
            return Ok(new { message = "Warehouse deleted" });
        }
    }
}
