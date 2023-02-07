using Microsoft.AspNetCore.Mvc;
using Service.GoodExchangeNote;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsExchangeNoteController : ControllerBase
    {
        private IGoodsExchangeNoteService _goodsExchangeNoteService;
        public GoodsExchangeNoteController(IGoodsExchangeNoteService goodsExchangeNoteService)
        {
            _goodsExchangeNoteService = goodsExchangeNoteService;
        }

        // GET: api/<GoodsExchangeNoteController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var goodssExchangeNote = _goodsExchangeNoteService.GetAll();
            return Ok(goodssExchangeNote);
        }

        // GET api/<GoodsExchangeNoteController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goodsExchangeNote = _goodsExchangeNoteService.GetById(id);
            return Ok(goodsExchangeNote);
        }

        // POST api/<GoodsController>
        [HttpPost("register")]
        public IActionResult Create(GoodsExchangeNoteCreateModel request)
        {
            _goodsExchangeNoteService.Create(request);
            return Ok(new { message = "Goods Exchange Note created" });
        }

        // PUT api/<GoodsExchangeNoteController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GoodsExchangeNoteUpdateModel request)
        {
            _goodsExchangeNoteService.Update(id, request);
            return Ok(new { message = "Goods Exchange Note updated" });
        }

        // DELETE api/<GoodsExchangeNoteController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _goodsExchangeNoteService.Delete(id);
            return Ok(new { message = "Goods Exchange Note deleted" });
        }
    }
}
