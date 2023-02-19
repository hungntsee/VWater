using Microsoft.AspNetCore.Mvc;
using Service.Manufacturers;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        private IManufacturerService _manufacturerService;
        public ManufacturerController(IManufacturerService manufacturerService)
        {
            _manufacturerService = manufacturerService;
        }

        // GET: api/<ManufacturerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var manufacturers = _manufacturerService.GetAll();
            return Ok(manufacturers);
        }

        // GET api/<ManufacturerController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var manufacturer = _manufacturerService.GetById(id);
            return Ok(manufacturer);
        }

        // POST api/<ManufacturerController>
        [HttpPost("register")]
        public IActionResult Create(ManufacturerCreateModel request)
        {
            _manufacturerService.Create(request);
            return Ok(new { message = "Manufacturer created" });
        }

        // PUT api/<ManufacturerController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, ManufacturerUpdateModel request)
        {
            _manufacturerService.Update(id, request);
            return Ok(new { message = "Manufacturer updated" });
        }

        // DELETE api/<ManufacturerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _manufacturerService.Delete(id);
            return Ok(new { message = "Manufacturer deleted" });
        }
    }
}
