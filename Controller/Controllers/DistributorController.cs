using Microsoft.AspNetCore.Mvc;
using Service.Distributors;
using VWater.Data.Entities;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private IDistributorService _distributorService;
        public DistributorController(IDistributorService distributorService)
        {
            _distributorService = distributorService;
        }

        // GET: api/<DistributorController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var distributors = _distributorService.GetAll();
            return Ok(distributors);
        }

        // GET api/<DistributorController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var distributor = _distributorService.GetById(id);
            return Ok(distributor);
        }

        // POST api/<DistributorController>
        [HttpPost("register")]
        public IActionResult Create(DistributorCreateModel request)
        {
            _distributorService.Create(request);
            return Ok(new { message = "Distributor created" });
        }

        // PUT api/<DistributorController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, DistributorUpdateModel request)
        {
            _distributorService.Update(id, request);
            return Ok(new { message = "Distributor updated" });
        }

        // DELETE api/<DistributorController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _distributorService.Delete(id);
            return Ok(new { message = "Distributor deleted" });
        }
    }
}
