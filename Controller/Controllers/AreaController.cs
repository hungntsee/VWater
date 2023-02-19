using Microsoft.AspNetCore.Mvc;
using Service.Areas;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaController : ControllerBase
    {
        private IAreaService _areaService;
        public AreaController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        // GET: api/<AreaController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var areas = _areaService.GetAll();
            return Ok(areas);
        }

        // GET api/<AreaController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var area = _areaService.GetById(id);
            return Ok(area);
        }

        // POST api/<AreaController>
        [HttpPost]
        public IActionResult Create([FromBody] AreaCreateModel request)
        {
            _areaService.Create(request);
            return Ok(new { message = "Area created" });
        }

        // PUT api/<AreaController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] AreaUpdateModel request)
        {
            _areaService.Update(id, request);
            return Ok(new { message = "Area updated" });
        }

        // DELETE api/<AreaController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _areaService.Delete(id);
            return Ok(new { message = "Area deleted" });
        }
    }
}
