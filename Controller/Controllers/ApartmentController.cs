using Microsoft.AspNetCore.Mvc;
using Service.Apartments;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentController : ControllerBase
    {
        private IApartmentService _apartmentService;
        public ApartmentController(IApartmentService apartmentService)
        {
            _apartmentService = apartmentService;
        }

        // GET: api/<ApartmentController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var apartments = _apartmentService.GetAll();
            return Ok(apartments);
        }

        // GET api/<ApartmentController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var apartment = _apartmentService.GetById(id);
            return Ok(apartment);
        }

        // POST api/<ApartmentController>
        [HttpPost("register")]
        public IActionResult Create(ApartmentCreateModel request)
        {
            _apartmentService.Create(request);
            return Ok(new { message = "Apartment created" });
        }

        // PUT api/<ApartmentController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, ApartmentUpdateModel request)
        {
            _apartmentService.Update(id, request);
            return Ok(new { message = "Apartment updated" });
        }

        // DELETE api/<ApartmentController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _apartmentService.Delete(id);
            return Ok(new { message = "Apartment deleted" });
        }
    }
}
