using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }
        // GET: api/<MenuControllerController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var menu = _menuService.GetAll();
            return Ok(menu);
        }

        // GET api/<MenuControllerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var menu = _menuService.GetById(id);
            return Ok(menu);
        }

        //GET api/<MenuController>/time
        [HttpGet("/api/Menu/Search")]
        public IActionResult GetByTime(DateTime time, int area_id)
        {
            var menu = _menuService.GetMenu(time, area_id);
            if(menu == null)
            {
                return BadRequest("Don't have this menu at that time!");
            }
            return Ok(menu);
        }

        // POST api/<MenuControllerController>
        [HttpPost]
        public IActionResult Create([FromBody] MenuCreateModel model)
        {
            _menuService.Create(model);
            return Ok(new { message = "Menu created" });
        }

        // PUT api/<MenuController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] MenuUpdateModel model)
        {
            _menuService.Update(id, model);
            return Ok(new { message = "Menu updated" });
        }

        // DELETE api/<WarehouseController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _menuService.Delete(id);
            return Ok(new { message = "Menu deleted" });
        }
    }
}
