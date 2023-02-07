using Microsoft.AspNetCore.Mvc;
using Service.Account;
using Service.Good;
using VWater.Data.Entities;
using VWater.Domain.Models;
using Service.GoodsInProducts;
using Repository.Domain.Models;
using Service.GoodsInProducts;

namespace Controller.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GoodsInProductController : ControllerBase
    {
        private IGoodsInProductService _goodsInProductService;
        public GoodsInProductController(IGoodsInProductService goodsInProductService)
        {
            _goodsInProductService = goodsInProductService;
        }

        // GET: api/<GoodsInProductController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var goodsInProducts = _goodsInProductService.GetAll();
            return Ok(goodsInProducts);
        }

        // GET api/<GoodsInProductController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goodsInProducts = _goodsInProductService.GetById(id);
            return Ok(goodsInProducts);
        }

        // POST api/<GoodsInProductController>
        [HttpPost("register")]
        public IActionResult Create(GoodsInProductCreateModel request)
        {
            _goodsInProductService.Create(request);
            return Ok(new { message = "Goods In Product created" });
        }

        // PUT api/<GoodsInProductController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GoodsInProductUpdateModel request)
        {
            _goodsInProductService.Update(id, request);
            return Ok(new { message = "Goods In Product updated" });
        }

        // DELETE api/<GoodsInProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _goodsInProductService.Delete(id);
            return Ok(new { message = "Goods In Product deleted" });
        }
    }
}
