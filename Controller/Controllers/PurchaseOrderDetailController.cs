using Microsoft.AspNetCore.Mvc;
using Service.Services;
using VWater.Domain.Models;

namespace Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderDetailController : ControllerBase
    {
        private IPurchaseOrderDetailService _purchasePurchaseOrderDetailService;

        public PurchaseOrderDetailController(IPurchaseOrderDetailService purchasePurchaseOrderDetailService)
        {
            _purchasePurchaseOrderDetailService = purchasePurchaseOrderDetailService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult GetAll()
        {
            var products = _purchasePurchaseOrderDetailService.GetAll();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var product = _purchasePurchaseOrderDetailService.GetById(id);
            return Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Create([FromBody] PurchaseOrderDetailCreateModel model)
        {
            _purchasePurchaseOrderDetailService.Create(model);
            return Ok(new { message = "PurchaseOrderDetail created" });
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PurchaseOrderDetailUpdateModel model)
        {
            _purchasePurchaseOrderDetailService.Update(id, model);
            return Ok(new { message = "PurchaseOrderDetail updated" });
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _purchasePurchaseOrderDetailService.Delete(id);
            return Ok(new { message = "PurchaseOrderDetail deleted" });
        }
    }
}
