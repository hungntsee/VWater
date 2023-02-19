using Microsoft.AspNetCore.Mvc;
using Service.GoodsInQuotations;
using VWater.Domain.Models;

namespace Controller.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class GoodsInQuotationController : ControllerBase
    {
        private IGoodsInQuotationService _goodsInQuotationService;
        public GoodsInQuotationController(IGoodsInQuotationService goodsInQuotationService)
        {
            _goodsInQuotationService = goodsInQuotationService;
        }

        // GET: api/<GoodsInQuotationController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var goodsInQuotations = _goodsInQuotationService.GetAll();
            return Ok(goodsInQuotations);
        }

        // GET api/<GoodsInQuotationController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var goodsInQuotations = _goodsInQuotationService.GetById(id);
            return Ok(goodsInQuotations);
        }

        // POST api/<GoodsInQuotationController>
        [HttpPost("register")]
        public IActionResult Create(GoodsInQuotationCreateModel request)
        {
            _goodsInQuotationService.Create(request);
            return Ok(new { message = "Goods In Quotation created" });
        }

        // PUT api/<GoodsInQuotationController>/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, GoodsInQuotationUpdateModel request)
        {
            _goodsInQuotationService.Update(id, request);
            return Ok(new { message = "Goods In Quotation updated" });
        }

        // DELETE api/<GoodsInQuotationController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _goodsInQuotationService.Delete(id);
            return Ok(new { message = "Goods In Quotation deleted" });
        }
    }
}
