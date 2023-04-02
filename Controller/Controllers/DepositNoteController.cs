using Microsoft.AspNetCore.Mvc;
using Service.DepositNotes;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepositNoteController : ControllerBase
    {
        private IDepositNoteService _depositNoteService;
        public DepositNoteController(IDepositNoteService depositNoteService)
        {
            _depositNoteService = depositNoteService;
        }

        // GET: api/<DepositNoteController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var depositNotes = _depositNoteService.GetAll();
            return Ok(depositNotes);
        }

        // GET api/<DepositNoteController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var depositNote = _depositNoteService.GetById(id);
            return Ok(depositNote);
        }

        // POST api/<DepositNoteController>
        [HttpPost]
        public IActionResult Create([FromBody] DepositNoteCreateModel request)
        {
            _depositNoteService.Create(request);
            return Ok(new { message = "DepositNote created" });
        }

        // PUT api/<DepositNoteController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DepositNoteUpdateModel request)
        {
            _depositNoteService.Update(id, request);
            return Ok(new { message = "DepositNote updated" });
        }

        // DELETE api/<DepositNoteController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _depositNoteService.Delete(id);
            return Ok(new { message = "DepositNote deleted" });
        }
    }
}
