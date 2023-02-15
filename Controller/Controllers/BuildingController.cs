using Microsoft.AspNetCore.Mvc;
using Service.Buildings;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private IBuildingService _buildingService;
        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        // GET: api/<BuildingController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var buildings = _buildingService.GetAll();
            return Ok(buildings);
        }

        // GET api/<BuildingController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var building = _buildingService.GetById(id);
            return Ok(building);
        }

        // POST api/<BuildingController>
        [HttpPost("register")]
        public IActionResult Create(BuildingCreateModel request)
        {
            _buildingService.Create(request);
            return Ok(new { message = "Building created" });
        }

        // PUT api/<BuildingController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, BuildingUpdateModel request)
        {
            _buildingService.Update(id, request);
            return Ok(new { message = "Building updated" });
        }

        // DELETE api/<BuildingController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _buildingService.Delete(id);
            return Ok(new { message = "Building deleted" });
        }
    }
}
