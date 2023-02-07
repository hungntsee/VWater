using Microsoft.AspNetCore.Mvc;
using Service.Account;
using Service.Good;
using VWater.Data.Entities;
using VWater.Domain.Models;
using Service.GoodsCompositions;
using Repository.Domain.Models;

namespace Controller.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GoodsCompositionController : ControllerBase
    {
        private IGoodsCompositionService _goodsCompositionService;
        public GoodsCompositionController(IGoodsCompositionService goodsCompositionService)
        {
            _goodsCompositionService = goodsCompositionService;
        }

        // GET: api/<GoodsCompositionController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var goodsCompositions = _goodsCompositionService.GetAll();
            return Ok(goodsCompositions);
        }

        // GET api/<GoodsCompositionController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goodsComposition = _goodsCompositionService.GetById(id);
            return Ok(goodsComposition);
        }

        // POST api/<GoodsController>
        [HttpPost("register")]
        public IActionResult Create(GoodsCompositionCreateModel request)
        {
            _goodsCompositionService.Create(request);
            return Ok(new { message = "Goods Composition created" });
        }

        // PUT api/<GoodsCompositionController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GoodsCompositionUpdateModel request)
        {
            _goodsCompositionService.Update(id, request);
            return Ok(new { message = "Goods Composition updated" });
        }

        // DELETE api/<GoodsCompositionController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _goodsCompositionService.Delete(id);
            return Ok(new { message = "Goods Composition deleted" });
        }
    }
}
