using Microsoft.AspNetCore.Mvc;
using Service.Good;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private IGoodsService _goodsService;
        public GoodsController(IGoodsService goodsService)
        {
            _goodsService = goodsService;
        }

        // GET: api/<GoodsController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var goodss = _goodsService.GetAll();
            return Ok(goodss);
        }

        // GET api/<GoodsController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goods = _goodsService.GetById(id);
            return Ok(goods);
        }

        // POST api/<GoodsController>
        [HttpPost("register")]
        public IActionResult Create(GoodsCreateModel request)
        {
            _goodsService.Create(request);
            return Ok(new { message = "Goods created" });
        }

        // PUT api/<GoodsController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id,GoodsUpdateModel request)
        {
            _goodsService.Update(id, request);
            return Ok(new { message = "Goods update" });
        }

        // DELETE api/<GoodsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _goodsService.Delete(id);
            return Ok(new { message = "Goods deleted" });
        }
    }
}
