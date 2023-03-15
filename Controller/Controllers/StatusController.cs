using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        IStatusService _statusService;

        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }

        // GET: api/<StatusController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var statuses = _statusService.GetAll();
            return Ok(statuses);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var status = _statusService.GetById(id);
            return Ok(status);
        }

        // POST api/<StatusController>
        [HttpPost]
        public IActionResult Create([FromBody] StatusCreateModel request)
        {
            _statusService.Create(request);
            return Ok(new { message = "Status created" });
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StatusUpdateModel request)
        {
            _statusService.Update(id, request);
            return Ok(new { message = "Status updated" });
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _statusService.Delete(id);
            return Ok(new { message = "Status deleted" });
        }
    }
}
